using EmployeeRepository.Model;
using System;
using System.Linq;

namespace EmployeeRepository.Utilities
{
    public static class DataUtil
    {
        public static void InitializeEmployees(IServiceProvider services)
        {

            if (services == null)
                return;
            using (var context = new EmployeeContext())
            { 
             
                if (context.Employees.Any())
                {
                    return;   // DB has already been populated
                }

                context.Employees.AddRange(
                   new Employee()
                   {
                       FirstName = "Karen",
                       MiddleName = "Marie",
                       LastName = "Gayda",
                       HireDate = DateTime.Today,

                       Address = "10222 Kashmere Ln",
                       City = "Escondido",
                       State = "California",
                       PostalCode = "92029",
                       Country = "US",
                       JobTitle = "Senior Software Engineer"
                   },
                   new Employee()
                   {
                       FirstName = "Robert",
                       LastName = "Sanchez",
                       HireDate = new DateTime(2001, 2, 3),
                       Address = "17890 Ivy St",
                       City = "Vista",
                       State = "California",
                       PostalCode = "92065",
                       Country = "US",
                       JobTitle = "Account Manager"
                   },
                   new Employee()
                   {
                       FirstName = "Maria",
                       MiddleName = "Conchita",
                       LastName = "Garcia",
                       HireDate = new DateTime(2015, 3, 19),

                       Address = "128 Pine St.",
                       City = "Temecula",
                       State = "California",
                       PostalCode = "92099",
                       Country = "US",
                       JobTitle = "Scrum Master"
                   },

                   new Employee()
                   {
                       FirstName = "Daniel",
                       MiddleName = "Marcus",
                       LastName = "Maedl",
                       HireDate = new DateTime(2012, 9, 9),
                       TerminationDate = new DateTime(2019, 10, 3),
                       Address = "3439 Activity Rd",
                       City = "Carlsbad",
                       State = "California",
                       PostalCode = "92008",
                       Country = "US",
                       JobTitle = "Facilities Supervisor"
                   });

                context.SaveChanges();
            }
        }                
    }
}
