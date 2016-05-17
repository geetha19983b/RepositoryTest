using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryTest.Models
{
    public class EmployeeDept
    {
      
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpAlias1 { get; set; }
        public string EmpAlias2 { get; set; }
        public string EmpAlias3 { get; set; }

        public int DepartmentID { get; set; }
        public string Name { get; set; }

    }
}