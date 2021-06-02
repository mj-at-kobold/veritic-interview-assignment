using System;
using System.Collections.Generic;

namespace DeveloperInterviewAssignment.BusinessObjects
{
    public interface ISalaryBreakDownCalculator
    {
        public SalaryBreakdown CalculateSalaryBreakDown(decimal salaryPackage, EPayFrequency payFrequency);
    }
}
