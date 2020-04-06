using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeRepository.Model;

namespace EmployeeRepository
{
    public class EmployeeContext : DbContext
    {
      
        public DbSet<Employee> Employees { get; set; }

        public EmployeeContext()
        { }

        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("EmployeeDB");
            }
        }


        public static DbContextOptions<EmployeeContext> GetInMemoryOptions(string testDbName = "EmployeeTest")
        {
            return new DbContextOptionsBuilder<EmployeeContext>()
                          .UseInMemoryDatabase(databaseName: testDbName)
                          .Options;
            
        }

        
    }
}
