// See https://aka.ms/new-console-template for more information
using Payroll.Business;
using System.IO;

Console.WriteLine("Hello, Welcome to Payroll!");
Console.WriteLine("");

var filePath = Path.Combine(Environment.CurrentDirectory, "PayrollData.txt");

PayrollEngine engine = new PayrollEngine(
    new FileSystemPayrollDataProvider() { FilePath = filePath }
);

if (File.Exists(filePath))
{
    var results = engine.CalculatePay();

    foreach (var result in results)
        Console.WriteLine($"The amount to pay {result.Name} is: {result.Amount} USD");
}
else
{
    Console.WriteLine($"File {filePath} was not found, please add a file and try again");
}

Console.ReadLine();