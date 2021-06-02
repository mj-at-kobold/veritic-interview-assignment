using DeveloperInterviewAssignment.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DeveloperInterviewAssignment
{
    [TestClass]
    public class SalaryBreakDown2021CalculatorTest
    {
        SalaryBreakdown salaryBreakdown;

        [TestInitialize]
        public void TestInitialise()
        {
            var x = new SalaryBreakownCalculator(new DeductionCalculator2021());
            salaryBreakdown = x.CalculateSalaryBreakDown(65000, EPayFrequency.Monthly);
        }
        [TestMethod]
        public void GrossPackage()
        {
            Assert.AreEqual(65000.00m, salaryBreakdown.GrossPackage);
        }

        [TestMethod]
        public void SuperAnnuation()
        {
            Assert.AreEqual(5639.27m, salaryBreakdown.SuperAnnuationContribution);
        }
        [TestMethod]
        public void TaxableIncome()
        {
            Assert.AreEqual(59360.73m, salaryBreakdown.TaxableIncome);
        }
        [TestMethod]
        public void MedicareLevy()
        {
            foreach(KeyValuePair<int, DeductionLineItem> kvp in salaryBreakdown.Deductions)
            {
                if (kvp.Value.DeductionName == "Medicare Levy")
                {
                    Assert.AreEqual(1188.00m, kvp.Value.DeductionAmount);
                    return;
                }
            };
        }
        [TestMethod]
        public void BudgetRepairLevy()
        {
            foreach (KeyValuePair<int, DeductionLineItem> kvp in salaryBreakdown.Deductions)
            {
                if (kvp.Value.DeductionName == "Budget Repair Levy")
                {
                    Assert.AreEqual(0.00m, kvp.Value.DeductionAmount);
                    return;
                }
            };
        }
        [TestMethod]
        public void IncomeTax()
        {
            foreach (KeyValuePair<int, DeductionLineItem> kvp in salaryBreakdown.Deductions)
            {
                if (kvp.Value.DeductionName == "Income Tax")
                {
                    Assert.AreEqual(10839.00m, kvp.Value.DeductionAmount);
                    return;
                }
            };
        }
        [TestMethod]
        public void NetIncome()
        {
            Assert.AreEqual(47333.73m, salaryBreakdown.NetIncome);
        }
        [TestMethod]
        public void PayPacket()
        {
            Assert.AreEqual(3944.48m, salaryBreakdown.PayPacketAmount);
        }

    }
}
