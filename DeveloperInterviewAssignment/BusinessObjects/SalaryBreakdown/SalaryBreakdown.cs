using System.Collections.Generic;

namespace DeveloperInterviewAssignment.BusinessObjects
{
    public class SalaryBreakdown
    {
        public SalaryBreakdown(decimal grossPackage, decimal superAnnuationContribution, decimal taxableIncome, SortedList<int, DeductionLineItem> deductions, decimal netIncome, decimal payPacketAmount)
        {
            GrossPackage = grossPackage;
            SuperAnnuationContribution = superAnnuationContribution;
            TaxableIncome = taxableIncome;
            Deductions = deductions;
            NetIncome = netIncome;
            PayPacketAmount = payPacketAmount;
        }

        public decimal GrossPackage { get; }

        public decimal SuperAnnuationContribution { get; }

        public decimal TaxableIncome { get; }

        public SortedList<int, DeductionLineItem> Deductions { get; }

        public decimal NetIncome { get; }

        public decimal PayPacketAmount { get; }
    }
}
