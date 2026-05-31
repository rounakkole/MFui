using MFui.Data;
using MFui.Data.Enums;
using TG.Blazor.IndexedDB;

namespace MFui.Services
{
    public class BacktestService : IBacktestService
    {
        private readonly IndexedDBManager _dbManager;
        private INAVResponseService navResponseService;

        public BacktestService(IndexedDBManager dbManager)
        {
            _dbManager = dbManager;
        }

        //CTOR
        public void NAVResponseCTOR(INAVResponseService _navResponseService)
        {
            this.navResponseService = _navResponseService;
        }






        public async Task RunInvestments(int _schemeCode)
        {
            InvestmentTable? investmentRecord;
            NAVData? navData;
            float navPrice;

            investmentRecord = await _dbManager.GetRecordById<int, InvestmentTable>("InvestmentTable", _schemeCode);

            DateTime startDate = investmentRecord.StartDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDate = (DateTime.UtcNow).Date;

            try
            {
                List<OrderTable> orderTables = await _dbManager.GetRecords<OrderTable>("OrderTable");
                if (orderTables != null)
                {
                    List<OrderTable> orderTableDelete = orderTables.Where(x => x.SchemeCode == investmentRecord.SchemeCode).ToList();
                    foreach (OrderTable order in orderTableDelete)
                    {
                        await _dbManager.DeleteRecord<int>("OrderTable", order.Id);
                    }
                }

                NAVResponse? navResponse = await navResponseService.GetNAVResponse(_schemeCode.ToString(), investmentRecord.StartDate, DateOnly.FromDateTime(endDate));

                DateTime date = startDate;
                while (date < endDate)
                {
                    navData = null;
                    DateTime dateLocal = date;
                    while (navData is null && dateLocal < endDate)
                    {
                        navData = navResponse?.Data.FirstOrDefault(x => x.Date == dateLocal.ToString("dd-MM-yyyy"));
                        /*
                        // Operator '>=' cannot be applied to operands of type 'string' and 'string'
                        navData = navResponse?.Data.Where(m => (m.Date >= (dateLocal.ToString("dd-MM-yyyy"))))
                                                   .OrderBy(m => m.Date) 
                                                   .FirstOrDefault();
                        */
                        dateLocal = dateLocal.AddDays(1);
                    }
                    if (navData is not null)
                    {
                        navPrice = float.Parse(navData.NAV);

                        OrderTable orderData = new OrderTable
                        {
                            SchemeCode = investmentRecord.SchemeCode,
                            SIPamount = investmentRecord.SIPamount,
                            NAV = navPrice,
                            TransactionDate = DateOnly.FromDateTime(dateLocal)
                        };

                        StoreRecord<OrderTable> newRecord = new StoreRecord<OrderTable>
                        {
                            Storename = "OrderTable",
                            Data = orderData
                        };

                        await _dbManager.AddRecord(newRecord);
                    }

                    date = calcStartDays(date, investmentRecord.Frequency);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task RunAllInvestments()
        {
            List<InvestmentTable> investmentsList = new();
            investmentsList = await _dbManager.GetRecords<InvestmentTable>("InvestmentTable");
            //if (investmentsList is not null && investmentsList.Count > 0)
            foreach (InvestmentTable investment in investmentsList)
            {
                //investment = investmentsList.Where(x => x.SchemeCode == _schemeCode);
                await RunInvestments(investment.SchemeCode);
            }
        }



        private DateTime calcStartDays(DateTime _startDate, Frequency _frequency)
        {
            DateTime retDateTime = _startDate.AddMonths(1);
            if (_frequency == Frequency.Daily)
            {
                retDateTime = _startDate.AddDays(1);
            }
            else if (_frequency == Frequency.Weekly)
            {
                retDateTime = _startDate.AddDays(7);
            }
            else if (_frequency == Frequency.BiWeekly)
            {
                retDateTime = _startDate.AddDays(14);
            }
            else if (_frequency == Frequency.Monthly)
            {
                retDateTime = _startDate.AddMonths(1);
            }
            else if (_frequency == Frequency.Yearly)
            {
                retDateTime = _startDate.AddYears(1);
            }

            //DateOnly retDateOnly = DateOnly.FromDateTime(retDateTime);
            return retDateTime;
        }

    }
}
