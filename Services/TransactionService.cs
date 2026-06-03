using MFui.Data;
using MFui.Data.Enums;
using TG.Blazor.IndexedDB;

namespace MFui.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IndexedDBManager _dbManager;
        private INAVResponseService navResponseService;

        public TransactionService(IndexedDBManager dbManager)
        {
            _dbManager = dbManager;
        }

        //CTOR
        public void NAVResponseCTOR(INAVResponseService _navResponseService)
        {
            this.navResponseService = _navResponseService;
        }






        public async Task CalculateTransactions(int _schemeCode)
        {
            InvestmentTable? investmentRecord = await _dbManager.GetRecordById<int, InvestmentTable>("InvestmentTable", _schemeCode);

            DateTime startDate = investmentRecord.StartDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDate = (DateTime.UtcNow).Date;
            DateTime dateLocal = startDate.AddDays(-1);
            float navPrice, lastNavPrice = 0;
            float quantity = 0;
            uint investedAmount = 0;

            try
            {
                await ClearTransactions(investmentRecord.SchemeCode);

                NAVResponse? navResponse = await navResponseService.GetNAVResponse(_schemeCode.ToString(), investmentRecord.StartDate, DateOnly.FromDateTime(endDate));

                for (DateTime date = startDate;  
                    date < endDate;
                    date = calcStartDays(date, investmentRecord.Frequency))
                {
                    
                    if (dateLocal >= date)
                    {
                        continue;
                    }

                    int yearsPassed = date.Year - investmentRecord.StartDate.Year;
                    if (DateOnly.FromDateTime(date) < investmentRecord.StartDate.AddYears(yearsPassed))
                    {
                        yearsPassed--;
                    }
                    double stepUpMultiplier = 1 + (investmentRecord.StepUp / 100.0);
                    double compoundedMultiplier = Math.Pow(stepUpMultiplier, yearsPassed);
                    uint currentSipAmount = (uint)Math.Round(investmentRecord.SIPamount * compoundedMultiplier);

                    dateLocal = date;
                    for (navPrice = 0;
                        (navPrice is 0 && (dateLocal < endDate));
                        dateLocal = dateLocal.AddDays(1))
                    {
                        NAVData? navData = navResponse?.Data.FirstOrDefault(x => x.Date == dateLocal.ToString("dd-MM-yyyy"));
                        /*
                        // Operator '>=' cannot be applied to operands of type 'string' and 'string'
                        navData = navResponse?.Data.Where(m => (m.Date >= (dateLocal.ToString("dd-MM-yyyy"))))
                                                   .OrderBy(m => m.Date) 
                                                   .FirstOrDefault();
                        */
                        if (navData is not null)
                        {
                            navPrice = float.Parse(navData.NAV);
                            break;
                        }
                    }
                    if (navPrice == 0)
                    {
                        continue;
                    }
                    OrderTable orderData = new OrderTable
                    {
                        SchemeCode = investmentRecord.SchemeCode,
                        SIPamount = currentSipAmount, // investmentRecord.SIPamount,
                        NAV = navPrice,
                        TransactionDate = DateOnly.FromDateTime(dateLocal)
                    };

                    StoreRecord<OrderTable> newRecord = new StoreRecord<OrderTable>
                    {
                        Storename = "OrderTable",
                        Data = orderData
                    };

                    await _dbManager.AddRecord(newRecord);

                    lastNavPrice = navPrice;
                    investedAmount += currentSipAmount;
                    quantity += (currentSipAmount / navPrice);
                }



                TotalsTable recordTotals = new TotalsTable
                {
                    Id = 2, // investmentRecord.SchemeCode 
                    InvestedAmount = investedAmount,
                    Returns = (float)(((quantity * lastNavPrice) - investedAmount) / investedAmount) * 100,
                    TotalValue = (uint)(quantity * lastNavPrice)
                };

                investmentRecord.TotalsTable = recordTotals;
                //investmentRecord.SIPamount = investmentRecord.SIPamount + (investmentRecord.StepUp * (uint)yearsPassed);
                StoreRecord<InvestmentTable> recordToSave = new StoreRecord<InvestmentTable>
                {
                    Storename = "InvestmentTable",
                    Data = investmentRecord
                };
                await _dbManager.UpdateRecord(recordToSave);

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task CalculateAllTransactions()
        {
            List<InvestmentTable> investmentsList = new();
            investmentsList = await _dbManager.GetRecords<InvestmentTable>("InvestmentTable");
            //if (investmentsList is not null && investmentsList.Count > 0)
            foreach (InvestmentTable investment in investmentsList)
            {
                //investment = investmentsList.Where(x => x.SchemeCode == _schemeCode);
                await CalculateTransactions(investment.SchemeCode);
            }
        }

        private async Task ClearTransactions(int _schemeCode)
        {
            List<OrderTable> orderTables = await _dbManager.GetRecords<OrderTable>("OrderTable");
            if (orderTables != null)
            {
                List<OrderTable> orderTableDelete = orderTables.Where(x => x.SchemeCode == _schemeCode).ToList();
                foreach (OrderTable order in orderTableDelete)
                {
                    await _dbManager.DeleteRecord<int>("OrderTable", order.Id);
                }
            }
        }

        public async Task ClearInvestments(int _schemeCode)
        {
            InvestmentTable? investmentRecord = await _dbManager.GetRecordById<int, InvestmentTable>("InvestmentTable", _schemeCode);

            if (investmentRecord != null)
            {
                await _dbManager.DeleteRecord<int>("InvestmentTable", investmentRecord.SchemeCode);
                await ClearTransactions(_schemeCode);
            }
        }

        private DateTime calcStartDays(DateTime _startDate, Frequency _frequency)
        {
            DateTime retDateTime = _startDate.AddMonths(1);
            switch (_frequency)
            {
                case Frequency.Daily: retDateTime = _startDate.AddDays(1); break;
                case Frequency.Weekly: retDateTime = _startDate.AddDays(7); break;
                case Frequency.BiWeekly: retDateTime = _startDate.AddDays(14); break;
                case Frequency.Monthly: retDateTime = _startDate.AddMonths(1); break;
                case Frequency.Quarterly: retDateTime = _startDate.AddMonths(3); break;
                case Frequency.Yearly: retDateTime = _startDate.AddYears(1); break;
            }

            //DateOnly retDateOnly = DateOnly.FromDateTime(retDateTime);
            return retDateTime;
        }

    }
}
