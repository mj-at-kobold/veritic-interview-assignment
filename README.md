# Author
Michael Jonauskis
## Coding Decisions
Upon reading the assignment I saw that there were two main parts for producing salary break down packages. That is
- A part that looks like it can change frequently. That is the deductions such as Medicare Levy, Budget Repair Levy and Income Tax. 
Also seeing it is a list as times go by deductions see likely to be added and removed.
- A part that looks like it will change less frequently, if at all. 
That is the calculation of the Gross package, Superannuation contribution, 
Taxable income, Net Income and Pay packet amount.

Taking this into consideration the output of the application is modelled in the SalaryBreakdown class.
Where the less frequently changed parts were mapped one to one with class properties (Gross Package, 
SuperAnnuationContribution, TaxableIncome, NetIncome, PaypacketAmount).
The deductions which seemed like they could change yearly were modelled out as property called Deductions 
which is a list. This was abstracted out so the list of deductions could be easily changed.

The actual salary breakdown calculations are broken down into two types of calculations
- Deductions (IDeductionCalculator) - Where a list of deductions is calculated along with a total amount based on the taxable income.
- Salary (ISalaryBreakDownCalculator) - Where the salary package is calculated based on the Gross Package and Pay Frequency

The implementation of these interfaces that produce the desired output are
- DeductionCalculator2021:
- SalaryBreakdownCalculator:


## Extending the application 
This code base utilises dependency injection and reflection in conjunction with custom attributes 
to make this program extensible and also more readable.

This code base has fully implemented a deductions engine based on the assumption that it would be updated yearly. 
This is done using reflection in conjuction with custom attributes. It is expected that when deductions rules 
change a new class derived from DeductionCalculator will be created just like the DeductionsCalculator2021. 

Dependency injection is used to choose current implementations of important interfaces e.g theDeductionsCalculator2021,
but it could just as easily have been DeductionsCalculator2020 if that class actually existed.

It was assumed that the non-deduction based calculations will be reasonably static. Hence not much work has been 
done making that to much extensible as compared with the deductions. Even though the deduction coding 
implementation could be copied.
