using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeveloperInterviewAssignmentUnitTests
{
    public class MedicareLevy2021
    {
        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(25000, 367)]
        [DataRow(40000, 800)]
        public void Test1(decimal TaxableIncome, decimal DeductionAmount)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(MedicareLevy2021.Amount(TaxableIncome), DeductionAmount)
        }
    }
}
