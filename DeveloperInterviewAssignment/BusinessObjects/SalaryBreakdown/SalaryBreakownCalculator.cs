using System;
using System.Collections.Generic;

namespace DeveloperInterviewAssignment.BusinessObjects
{
    public class SalaryBreakownCalculator : ISalaryBreakDownCalculator
    {
        #region Constants
        private const decimal NumberOfWeeksInAYear = 52.0m;
        private const decimal NumberOfFortnightsInAYear = 26.0m;
        private const decimal NumberOfMonthsInAYear = 12.0m;
        private const decimal SuperContributionPercentage = 0.095m;
        #endregion

        #region Fields
        private EPayFrequency payFrequency;

        private decimal grossPackage;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SalaryBreakownCalculator"/> class.
        /// </summary>
        /// <param name="deductionCalculator">This parameter.</param>
        /// <remarks>
        /// An instance of this object should never be directly created. It is expected to be created by the dependency injector located in <see cref="Program"/>.
        /// </remarks>
        public SalaryBreakownCalculator(IDeductionCalculator deductionCalculator)
        {
            DeductionCalculator = deductionCalculator;
        }
        #endregion

        #region Private methods and properties
        private IDeductionCalculator DeductionCalculator { get; }

        private decimal SuperAnnuationContribution => Math.Round((grossPackage / (1 + SuperContributionPercentage)) * SuperContributionPercentage, 2);

        private decimal TaxableIncome => Math.Round(grossPackage - SuperAnnuationContribution, 2);

        private decimal NetIncome => grossPackage - SuperAnnuationContribution - DeductionCalculator.TotalDeductions;

        private decimal PayPacketAmount => Math.Round(NetIncome / NumberOfPayIntervalsPerYear, 2);

        private decimal NumberOfPayIntervalsPerYear
        {
            get
            {
                switch (payFrequency)
                {
                    case EPayFrequency.Weekly:
                        return NumberOfWeeksInAYear;
                    case EPayFrequency.Fortnightly:
                        return NumberOfFortnightsInAYear;
                    case EPayFrequency.Monthly:
                        return NumberOfMonthsInAYear;
                    default:
                        return NumberOfMonthsInAYear;
                }
            }
        }

        private SortedList<int, DeductionLineItem> DeductionList => DeductionCalculator.DeductionList;

        #endregion

        #region Public methods and properties
        public SalaryBreakdown CalculateSalaryBreakDown(decimal salaryPackage, EPayFrequency payFrequency)
        {
            grossPackage = Math.Ceiling(salaryPackage);
            this.payFrequency = payFrequency;
            DeductionCalculator.SetTaxableIncome(TaxableIncome);

            return new SalaryBreakdown(grossPackage, SuperAnnuationContribution, TaxableIncome, DeductionList, NetIncome, PayPacketAmount);
        }
        #endregion
    }
}
