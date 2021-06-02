using DeveloperInterviewAssignment.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeveloperInterviewAssignment
{
    [TestClass]
    public class Deductions2021Test
    {
        [TestMethod]
        [DataRow(0.0, 0.0)]
        [DataRow(25000.0, 1292.0)]
        [DataRow(45000.0, 6172.0)]
        [DataRow(95000.0, 22782.0)]
        [DataRow(200000.0, 63632.0)]
        public void TaxableIncome(double TaxableIncome, double DeductionAmount)
        {
            DeductionCalculator2021 deductionCalculator = new DeductionCalculator2021();
            deductionCalculator.SetTaxableIncome((decimal)TaxableIncome);
            Assert.AreEqual((decimal)DeductionAmount, deductionCalculator.TaxableIncomeAmount);
        }

        [TestMethod]
        [DataRow(0.0, 0.0)]
        [DataRow(25000.0, 367.0)]
        [DataRow(40000.0, 800.0)]
        public void MedicareLevy(double TaxableIncome, double DeductionAmount)
        {
            DeductionCalculator2021 deductionCalculator = new DeductionCalculator2021();
            deductionCalculator.SetTaxableIncome((decimal)TaxableIncome);
            Assert.AreEqual((decimal)DeductionAmount, deductionCalculator.MedicareLevyAmount);
        }

        [TestMethod]
        [DataRow(0.0, 0.0)]
        [DataRow(200000.0, 400.0)]
        public void BudgetRepairLevy(double TaxableIncome, double DeductionAmount)
        {
            DeductionCalculator2021 deductionCalculator = new DeductionCalculator2021();
            deductionCalculator.SetTaxableIncome((decimal)TaxableIncome);
            Assert.AreEqual((decimal)DeductionAmount, deductionCalculator.BudgetRepariLevyAmount);
        }

        [TestMethod]
        [DataRow(0.0)]
        [DataRow(25000.0)]
        [DataRow(45000.0)]
        [DataRow(95000.0)]
        [DataRow(200000.0)]
        public void TotalDeductions(double TaxableIncome)
        {
            DeductionCalculator2021 deductionCalculator = new DeductionCalculator2021();
            deductionCalculator.SetTaxableIncome((decimal)TaxableIncome);
            decimal totalDeductions = deductionCalculator.BudgetRepariLevyAmount +
                                      deductionCalculator.TaxableIncomeAmount +
                                      deductionCalculator.MedicareLevyAmount;

            Assert.AreEqual(totalDeductions, deductionCalculator.TotalDeductions);
        }
    }
}
