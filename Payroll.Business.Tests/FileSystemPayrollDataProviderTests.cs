namespace Payroll.Business.Tests
{
    public class FileSystemPayrollDataProviderTests
    {
        private FileSystemPayrollDataProvider _parser;
        private string _testPayrollFilePath;

        [SetUp]
        public void Setup()
        {
            _testPayrollFilePath = Path.Combine(Environment.CurrentDirectory, "TestPayroll.txt");
            _parser = new FileSystemPayrollDataProvider() { FilePath = _testPayrollFilePath };           
        }

        [Test]
        public void GetPayrollData_EmptyFile()
        {
            WriteLinesToTestFile(
                new string[]
                {
                    ""
                }
            );

            var results = _parser.GetPayrollData();

            Assert.That(results.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetPayrollData_OneEmployeeWithOneInvalidShift()
        {
            WriteLinesToTestFile(
                new string[]
                {
                    "RENE=MO10:00-1200"
                }
            );

            var results = _parser.GetPayrollData();

            Assert.That(results.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetPayrollData_OneEmployeeWithOneInvalidShiftAndOneValidShift()
        {
            WriteLinesToTestFile(
                new string[]
                {
                    "RENE=MO10:00-12:00,TU10:0012:00"
                }
            );

            var results = _parser.GetPayrollData();

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].WorkedShifts.Count, Is.EqualTo(1));
            Assert.That(results[0].WorkedShifts[0].DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(results[0].WorkedShifts[0].StartHour, Is.EqualTo(10));
            Assert.That(results[0].WorkedShifts[0].EndHour, Is.EqualTo(12));
        }

        [Test]
        public void GetPayrollData_OneEmployeeWithOneShift()
        {
            WriteLinesToTestFile(
                new string[]
                {
                    "RENE=MO10:00-12:00"
                }
            );

            var results = _parser.GetPayrollData();

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].WorkedShifts.Count, Is.EqualTo(1));
            Assert.That(results[0].WorkedShifts[0].DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(results[0].WorkedShifts[0].StartHour, Is.EqualTo(10));
            Assert.That(results[0].WorkedShifts[0].EndHour, Is.EqualTo(12));
        }

        [Test]
        public void GetPayrollData_OneEmployeeWithFiveShifts()
        {
            WriteLinesToTestFile(
                new string[]
                {
                    "RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00"
                }
            );

            var results = _parser.GetPayrollData();

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].WorkedShifts.Count, Is.EqualTo(5));

            Assert.That(results[0].WorkedShifts[0].DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(results[0].WorkedShifts[0].StartHour, Is.EqualTo(10));
            Assert.That(results[0].WorkedShifts[0].EndHour, Is.EqualTo(12));

            Assert.That(results[0].WorkedShifts[1].DayOfWeek, Is.EqualTo(DayOfWeek.Tuesday));
            Assert.That(results[0].WorkedShifts[1].StartHour, Is.EqualTo(10));
            Assert.That(results[0].WorkedShifts[1].EndHour, Is.EqualTo(12));

            Assert.That(results[0].WorkedShifts[2].DayOfWeek, Is.EqualTo(DayOfWeek.Thursday));
            Assert.That(results[0].WorkedShifts[2].StartHour, Is.EqualTo(1));
            Assert.That(results[0].WorkedShifts[2].EndHour, Is.EqualTo(3));

            Assert.That(results[0].WorkedShifts[3].DayOfWeek, Is.EqualTo(DayOfWeek.Saturday));
            Assert.That(results[0].WorkedShifts[3].StartHour, Is.EqualTo(14));
            Assert.That(results[0].WorkedShifts[3].EndHour, Is.EqualTo(18));

            Assert.That(results[0].WorkedShifts[4].DayOfWeek, Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(results[0].WorkedShifts[4].StartHour, Is.EqualTo(20));
            Assert.That(results[0].WorkedShifts[4].EndHour, Is.EqualTo(21));
        }

        [Test]
        public void GetPayrollData_TwoEmployeesWithMultipleShifts()
        {
            WriteLinesToTestFile(
                new string[]
                {
                    "RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00",
                    "ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00"
                }
            );

            var results = _parser.GetPayrollData();

            Assert.That(results.Count(), Is.EqualTo(2));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].WorkedShifts.Count, Is.EqualTo(5));

            Assert.That(results[0].WorkedShifts[0].DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(results[0].WorkedShifts[0].StartHour, Is.EqualTo(10));
            Assert.That(results[0].WorkedShifts[0].EndHour, Is.EqualTo(12));

            Assert.That(results[0].WorkedShifts[1].DayOfWeek, Is.EqualTo(DayOfWeek.Tuesday));
            Assert.That(results[0].WorkedShifts[1].StartHour, Is.EqualTo(10));
            Assert.That(results[0].WorkedShifts[1].EndHour, Is.EqualTo(12));

            Assert.That(results[0].WorkedShifts[2].DayOfWeek, Is.EqualTo(DayOfWeek.Thursday));
            Assert.That(results[0].WorkedShifts[2].StartHour, Is.EqualTo(1));
            Assert.That(results[0].WorkedShifts[2].EndHour, Is.EqualTo(3));

            Assert.That(results[0].WorkedShifts[3].DayOfWeek, Is.EqualTo(DayOfWeek.Saturday));
            Assert.That(results[0].WorkedShifts[3].StartHour, Is.EqualTo(14));
            Assert.That(results[0].WorkedShifts[3].EndHour, Is.EqualTo(18));

            Assert.That(results[0].WorkedShifts[4].DayOfWeek, Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(results[0].WorkedShifts[4].StartHour, Is.EqualTo(20));
            Assert.That(results[0].WorkedShifts[4].EndHour, Is.EqualTo(21));

            Assert.That(results[1].Name, Is.EqualTo("ASTRID"));
            Assert.That(results[1].WorkedShifts.Count, Is.EqualTo(3));

            Assert.That(results[1].WorkedShifts[0].DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(results[1].WorkedShifts[0].StartHour, Is.EqualTo(10));
            Assert.That(results[1].WorkedShifts[0].EndHour, Is.EqualTo(12));

            Assert.That(results[1].WorkedShifts[1].DayOfWeek, Is.EqualTo(DayOfWeek.Thursday));
            Assert.That(results[1].WorkedShifts[1].StartHour, Is.EqualTo(12));
            Assert.That(results[1].WorkedShifts[1].EndHour, Is.EqualTo(14));

            Assert.That(results[1].WorkedShifts[2].DayOfWeek, Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(results[1].WorkedShifts[2].StartHour, Is.EqualTo(20));
            Assert.That(results[1].WorkedShifts[2].EndHour, Is.EqualTo(21));
        }

        [TearDown]
        public void TearDown()
        {
            System.IO.File.Delete(_testPayrollFilePath);
        }

        private void WriteLinesToTestFile(string[] lines)
        {
            using (StreamWriter outputFile = new StreamWriter(_testPayrollFilePath))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }
    }
}