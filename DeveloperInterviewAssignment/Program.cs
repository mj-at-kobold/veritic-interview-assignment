using System;
using System.Collections.Generic;
using DeveloperInterviewAssignment.BusinessObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeveloperInterviewAssignment
{
    public class Program
    {
        #region Private fields
        private static decimal suppliedSalaryPackageAmount = 0.0m;
        private static EPayFrequency suppliedPaymentFrequency = EPayFrequency.Weekly;
        private static SalaryBreakdown calculatedSalaryBreakdown;
        private static IHost host;
        #endregion

        public static void Main(string[] args)
        {
            SetupDependencyInjection();
            try
            {
                GetSalaryAndPayFrequency();
                CalculateSalaryDetails();
                DisplaySalaryDetails();
            }
            catch (Exception e)
            {
                DisplayErrorDetails(e);
            }

            WaitForUserToEndProgram();
        }

        #region Private properties and methods
        private static void GetSalaryAndPayFrequency()
        {
            Console.Write("Enter your salary package amount: ");
            string salaryPackageAmountInput = Console.ReadLine();
            if (!decimal.TryParse(salaryPackageAmountInput, out suppliedSalaryPackageAmount))
            {
                throw new Exception($"The salary package supplied{salaryPackageAmountInput} is not a valid number. Please type in only valid decimal number with no commas and $ signs. Such as 65000 or 200000");
            }

            Console.Write("Enter your pay frequency (W for weekly, F for fortnighly, M for monthly): ");
            string payFrequencyInput = Console.ReadLine();

            suppliedPaymentFrequency = EPayFrequency.Weekly;
            switch (payFrequencyInput.ToUpper())
            {
                case "W":
                    suppliedPaymentFrequency = EPayFrequency.Weekly;
                    break;
                case "F":
                    suppliedPaymentFrequency = EPayFrequency.Fortnightly;
                    break;
                case "M":
                    suppliedPaymentFrequency = EPayFrequency.Monthly;
                    break;
                default:
                    throw new Exception($"{payFrequencyInput} is not a valid payment frequency option please select (W for weekly, F for fortnighly, M for monthly).");
            }
        }

        private static void CalculateSalaryDetails()
        {
            ISalaryBreakDownCalculator salaryBreakownCalculator = host.Services.GetRequiredService<ISalaryBreakDownCalculator>();
            calculatedSalaryBreakdown = salaryBreakownCalculator.CalculateSalaryBreakDown(suppliedSalaryPackageAmount, suppliedPaymentFrequency);
        }

        private static void DisplaySalaryDetails()
        {
            string currencyFormat = "$#,##0.00;-$#,##0.00;$0";
            string grossPackageCurrencyFormat = "$#,##0";

            Console.WriteLine();
            Console.WriteLine("Calculating salary details...");
            Console.WriteLine();
            Console.WriteLine($"Gross package: {calculatedSalaryBreakdown.GrossPackage.ToString(grossPackageCurrencyFormat)}");
            Console.WriteLine($"Superannuation: {calculatedSalaryBreakdown.SuperAnnuationContribution.ToString(currencyFormat)}");
            Console.WriteLine();
            Console.WriteLine($"Taxable Income: {calculatedSalaryBreakdown.TaxableIncome.ToString(currencyFormat)}");
            Console.WriteLine();
            Console.WriteLine("Deductions:");
            foreach (KeyValuePair<int, DeductionLineItem> kvp in calculatedSalaryBreakdown.Deductions)
            {
                Console.WriteLine($"{kvp.Value.DeductionName}: {kvp.Value.DeductionAmount.ToString(currencyFormat)}");
            }

            Console.WriteLine();
            Console.WriteLine($"Net income: {calculatedSalaryBreakdown.NetIncome.ToString(currencyFormat)}");
            Console.WriteLine($"Pay Packet: {calculatedSalaryBreakdown.PayPacketAmount.ToString(currencyFormat)} {PayFrequency(suppliedPaymentFrequency)}");
            Console.WriteLine();
        }

        private static void WaitForUserToEndProgram()
        {
            Console.Write("Press any key to end...");
            Console.ReadKey();
        }

        private static void DisplayErrorDetails(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        private static IHost SetupDependencyInjection() =>
            host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                    services.AddScoped<IDeductionCalculator, DeductionCalculator2021>()
                            .AddScoped<ISalaryBreakDownCalculator, SalaryBreakownCalculator>()
                            .AddLogging((builder) =>
                            {
                                builder.AddFilter("Microsoft", LogLevel.Warning)
                                       .AddConsole();
                            })).Build();

        private static string PayFrequency(EPayFrequency payFrequency)
        {
            switch (payFrequency)
            {
                case EPayFrequency.Weekly:
                    return "per week";
                case EPayFrequency.Fortnightly:
                    return "per fortnight";
                case EPayFrequency.Monthly:
                    return "per month";
            }

            return string.Empty;
        }
        #endregion
    }
}
