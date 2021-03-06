﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRepository.Model
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public string Address { get; set; }
        public string AptNumber { get; set; }
       
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public string PostalCode { get; set; }
        public string JobTitle { get; set; }
    }
}
