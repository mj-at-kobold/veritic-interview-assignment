using System.Collections.Generic;

namespace DeveloperInterviewAssignment.BusinessObjects
{
    public interface IDeductionCalculator
    {
        public decimal TotalDeductions { get; }

        public SortedList<int, DeductionLineItem> DeductionList { get; }

        public void SetTaxableIncome(decimal taxableIncome);
    }
}
