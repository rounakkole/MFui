namespace MFui.Data
{
    public class TotalsTable
    {
        public byte Id { get; set; } = 1; // Key 

        public uint InvestedAmount { get; set; } = 0;
        public float Returns { get; set; } = 0;
        public uint TotalValue { get; set; } = 0;
    }
}
