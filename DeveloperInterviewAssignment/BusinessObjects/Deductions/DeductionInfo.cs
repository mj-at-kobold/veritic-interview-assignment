using System;

namespace DeveloperInterviewAssignment.BusinessObjects.Deductions
{
    /// <summary>
    /// Add this attribute to any property that returns a decimal, on any class that derives from DeductionCalculator
    /// so that it gets added as a deduction. Then is used to calculate TotalDeductions value and also adds
    /// a Deduction Line Item to the deduction list.
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
    /// </example>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DeductionInfo : Attribute
    {
        public DeductionInfo(string deductionName, int displayOrder)
        {
            DeductionName = deductionName;
            DisplayOrder = displayOrder;
        }

        public string DeductionName { get; }

        public int DisplayOrder { get; }
    }
}
