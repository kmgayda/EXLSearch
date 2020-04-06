using NUnit.Framework;
using EmployeeRepository;
using EmployeeRepository.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeSearchTest
{
    public class Tests
    {
        public DbContextOptions<EmployeeContext> Options { get; set; }

        [SetUp]
        public void Setup()
        {
            Options = EmployeeContext.GetInMemoryOptions("Employees");
        }

        [Test]
        public async Task AddEmployee()
        {
            using (var context = new EmployeeContext(Options))
            {
                var employee = new Employee()
                {
                    FirstName = "Karen",
                    MiddleName = "Marie",
                    LastName = "Gayda",
                    HireDate = System.DateTime.Today,
                   
                    Address = "10222 Kashmere Ln",
                    City = "Escondido",
                    State = "California",
                    PostalCode = "92029",
                    Country = "US",
                    JobTitle = "Senior Software Engineer"
                };

                context.Employees.Add(employee);
                context.SaveChanges();
            }

            using (var context = new EmployeeContext(Options))
            {
                var count = await context.Employees.CountAsync();
                Assert.AreEqual(1, count);
                var employee = context.Employees.FirstOrDefaultAsync(e => e.FirstName == "Karen");

                Assert.IsNotNull(employee);

                Assert.IsTrue(employee.Id > 0);
            }
        }
    }
}