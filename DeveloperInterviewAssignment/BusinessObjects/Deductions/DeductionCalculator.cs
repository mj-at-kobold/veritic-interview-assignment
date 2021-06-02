using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperInterviewAssignment.BusinessObjects.Deductions
{
    /// <summary>
    /// Derive all deduction calculators from this object as it automatically calculates the
    ///   * TotalDeductions amounts
    ///   * and list of deductions
    /// Based on all decimal properties that have the custom attribute DeductionInfo.
    /// </summary>
    /// <example>
    /// <code>
    /// [DeductionInfo("Medicare Levy", 1)]
    /// public decimal CO2Level
    /// {
    ///    get {
    ///     return 100m
    ///    }
    /// }
    /// </code>
    /// </example>{
    public class DeductionCalculator : IDeductionCalculator
    {
        public decimal TaxableIncome => Math.Floor(UnroundedTaxableIncome);

        public SortedList<int, DeductionLineItem> DeductionList => Deductions;

        public decimal TotalDeductions => SumofDeductions;

        private decimal UnroundedTaxableIncome { get; set; } = 0.0m;

        private SortedList<int, DeductionLineItem> Deductions { get; set; } = new SortedList<int, DeductionLineItem>();

        private decimal SumofDeductions { get; set; } = 0.0m;

        public void SetTaxableIncome(decimal taxableIncome)
        {
            UnroundedTaxableIncome = taxableIncome;
            CalculateDeductions();
        }

        private void CalculateDeductions()
        {
            SumofDeductions = 0.00m;
            Deductions.Clear();

            Type t = this.GetType();
            t.GetProperties()
             .ToList()
             .ForEach(m =>
             {
                 m.GetCustomAttributes(true)
                  .ToList()
                  .ForEach(ca =>
                  {
                      if (ca.GetType().Equals(typeof(DeductionInfo)) && m.PropertyType.Equals(typeof(decimal)))
                      {
                          DeductionInfo deductionInfo = (DeductionInfo)ca;

                          var emptyParameters = new object[] { };
                          decimal deductionAmount = (decimal)m.GetValue(this);

                          SumofDeductions += deductionAmount;
                          Deductions.Add(
                              deductionInfo.DisplayOrder,
                              new DeductionLineItem(deductionInfo.DeductionName, deductionAmount));
                      }
                  });
             });
        }
    }
}
