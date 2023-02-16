# Payroll Solution Overview
- This solution was implemented using .NET 6 and C#.
- The software development process used was TDD.

# Architecture
- **Payroll.Business**: Contains the business logic for the application and core abstractions.
- **Payroll.Console**: Contains the code for the console applitacion and has a source code dependency on Payroll.Business.
- **Payroll.Business.Tests**: Contains the code with unit tests for Payroll.Business.

# Steps to Execute
1. Clone the repo on your local or download the zip containing the source code
2. Open a command prompt on the folder Payroll.Console and run the "dotnet run" command

**Note**: In order to run the applitacion you need to install the .NET 6 SDK
