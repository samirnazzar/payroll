namespace Payroll.Business.Tests
{
    public class PayrollEngineTests
    {
        private PayrollEngine _engine;
        private FakePayrollDataProvider _fakeDataProvider;

        [SetUp]
        public void Setup()
        {
            _fakeDataProvider = new FakePayrollDataProvider();
            _engine = new PayrollEngine(_fakeDataProvider);
        }

        [Test]
        public void OneEmployeeWithOneShift()
        {
            var payrollData = new List<EmployeeWorkedInfo>();
            payrollData.Add(
                new EmployeeWorkedInfo()
                {
                    Name = "RENE",
                    WorkedShifts = new List<WorkedShift>()
                    {
                        new WorkedShift(DayOfWeek.Monday, 10, 12)
                    }
                }
            );
            _fakeDataProvider.PayrollData = payrollData;

            var results = _engine.CalculatePay();

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].Amount, Is.EqualTo(30));
        }

        [Test]
        public void OneEmployeeWithOneSpanningShift()
        {
            var payrollData = new List<EmployeeWorkedInfo>();
            payrollData.Add(
                new EmployeeWorkedInfo()
                {
                    Name = "RENE",
                    WorkedShifts = new List<WorkedShift>()
                    {
                        new WorkedShift(DayOfWeek.Monday, 17, 21)
                    }
                }
            );
            _fakeDataProvider.PayrollData = payrollData;

            var results = _engine.CalculatePay();

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].Amount, Is.EqualTo(75));
        }

        [Test]
        public void OneEmployeeWithTwoShifts()
        {
            var payrollData = new List<EmployeeWorkedInfo>();
            payrollData.Add(
                new EmployeeWorkedInfo()
                {
                    Name = "RENE",
                    WorkedShifts = new List<WorkedShift>()
                    {
                        new WorkedShift(DayOfWeek.Monday, 10, 12),
                        new WorkedShift(DayOfWeek.Tuesday, 10, 12)
                    }
                }
            );
            _fakeDataProvider.PayrollData = payrollData;

            var results = _engine.CalculatePay();

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].Amount, Is.EqualTo(60));
        }

        [Test]
        public void OneEmployeeWithFiveShifts()
        {
            var payrollData = new List<EmployeeWorkedInfo>();
            payrollData.Add(
                new EmployeeWorkedInfo()
                {
                    Name = "RENE",
                    WorkedShifts = new List<WorkedShift>()
                    {
                        new WorkedShift(DayOfWeek.Monday, 10, 12),
                        new WorkedShift(DayOfWeek.Tuesday, 10, 12),
                        new WorkedShift(DayOfWeek.Thursday, 1, 3),
                        new WorkedShift(DayOfWeek.Saturday, 14, 18),
                        new WorkedShift(DayOfWeek.Sunday, 20, 21)
                    }
                }
            );
            _fakeDataProvider.PayrollData = payrollData;

            var results = _engine.CalculatePay();

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].Amount, Is.EqualTo(215));
        }

        [Test]
        public void TwoEmployeesEachWithThreeShifts()
        {
            var payrollData = new List<EmployeeWorkedInfo>();
            payrollData.Add(
                new EmployeeWorkedInfo()
                {
                    Name = "RENE",
                    WorkedShifts = new List<WorkedShift>()
                    {
                        new WorkedShift(DayOfWeek.Tuesday, 10, 12),
                        new WorkedShift(DayOfWeek.Saturday, 14, 18),
                        new WorkedShift(DayOfWeek.Sunday, 20, 21)
                    }
                }
            );
            payrollData.Add(
                new EmployeeWorkedInfo()
                {
                    Name = "ASTRID",
                    WorkedShifts = new List<WorkedShift>()
                    {
                        new WorkedShift(DayOfWeek.Monday, 10, 12),
                        new WorkedShift(DayOfWeek.Thursday, 12, 14),
                        new WorkedShift(DayOfWeek.Sunday, 20, 21)
                    }
                }
            );
            _fakeDataProvider.PayrollData = payrollData;

            var results = _engine.CalculatePay();

            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].Amount, Is.EqualTo(135));
            Assert.That(results[1].Name, Is.EqualTo("ASTRID"));
            Assert.That(results[1].Amount, Is.EqualTo(85));
        }

        [Test]
        public void OneEmployeeWithTwoShiftsOnSameDay()
        {
            var payrollData = new List<EmployeeWorkedInfo>();
            payrollData.Add(
                new EmployeeWorkedInfo()
                {
                    Name = "RENE",
                    WorkedShifts = new List<WorkedShift>()
                    {
                        new WorkedShift(DayOfWeek.Monday, 10, 12),
                        new WorkedShift(DayOfWeek.Monday, 17, 21)
                    }
                }
            );
            _fakeDataProvider.PayrollData = payrollData;

            var results = _engine.CalculatePay();

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("RENE"));
            Assert.That(results[0].Amount, Is.EqualTo(105));
        }
    }
}