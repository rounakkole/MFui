
namespace MFui.Data
{
    public class OrderTable
    {
        public int Id { get; set; } // Key

        public int SchemeCode { get; set; } 

        public uint SIPamount { get; set; } = 0;
        public float NAV { get; set; } = 0;

        public DateOnly TransactionDate { get; set; } 

    }
}
