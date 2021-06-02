using System;
using System.Collections.Generic;
using System.Text;
using DeveloperInterviewAssignment.BusinessObjects.Deductions;

namespace DeveloperInterviewAssignment.BusinessObjects
{
    public sealed class DeductionCalculator2021 : DeductionCalculator
    {
        [DeductionInfo("Medicare Levy", 1)]
        public decimal MedicareLevyAmount
        {
            get
            {
                if (TaxableIncome >= 21336 && TaxableIncome < 26668)
                {
                    return Math.Ceiling((TaxableIncome - 21335m) * 0.1m);
                }

                if (TaxableIncome >= 26669)
                {
                    return Math.Ceiling(TaxableIncome * 0.02m);
                }

                return 0;
            }
        }

        [DeductionInfo("Budget Repair Levy", 2)]
        public decimal BudgetRepariLevyAmount
        {
            get
            {
                if (TaxableIncome >= 180001m)
                {
                    return (TaxableIncome - 180000m) * 0.02m;
                }

                return 0;
            }
        }

        [DeductionInfo("Income Tax", 3)]
        public decimal TaxableIncomeAmount
        {
            get
            {
                if (TaxableIncome >= 18201m && TaxableIncome <= 37000m)
                {
                    return (TaxableIncome - 18200m) * 0.19m;
                }

                if (TaxableIncome >= 37001m && TaxableIncome <= 87000m)
                {
                    return ((TaxableIncome - 37000m) * 0.325m) + 3572m;
                }

                if (TaxableIncome >= 87001m && TaxableIncome <= 180000m)
                {
                    return ((TaxableIncome - 87000m) * 0.37m) + 19822m;
                }

                if (TaxableIncome >= 180001m)
                {
                    return ((TaxableIncome - 180000m) * 0.47m) + 54232m;
                }

                return 0;
            }
        }
    }
}
