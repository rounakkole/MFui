using MFui.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MFui.Data
{
    public class InvestmentTable
    {
        public int SchemeCode { get; set; } // Key
        public string SchemeName { get; set; } = string.Empty;

        public Frequency Frequency { get; set; } = Frequency.Monthly;


        [Range(99, uint.MaxValue, ErrorMessage = "Value must be at least 100.")]
        public uint SIPamount { get; set; } = 0;

        [Required(ErrorMessage = "Start date is required.")]
        [PastDate(ErrorMessage = "Start date must be today or in the past.")]
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public byte StepUp { get; set; } = 0;

        public TotalsTable? TotalsTable { get; set; } = default; // Collection

    }
}
