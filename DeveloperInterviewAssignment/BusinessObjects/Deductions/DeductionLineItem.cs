namespace DeveloperInterviewAssignment.BusinessObjects
{
    public class DeductionLineItem
    {
        public DeductionLineItem(string deductionName, decimal deductionAmount)
        {
            DeductionName = deductionName;
            DeductionAmount = deductionAmount;
        }

        public string DeductionName { get; }

        public decimal DeductionAmount { get; }
    }
}
