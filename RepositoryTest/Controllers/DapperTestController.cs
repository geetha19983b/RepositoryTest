using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infosys.FoundationLibrary.DataAccess.GenericAbstract;
using RepositoryTest.Models;
using Infosys.FoundationLibrary.DataAccess.GenericInterface;
//using System.Data;

namespace RepositoryTest.Controllers
{
   
    public class DapperTestController : Controller
    {
        IGenericDataAccess dapperobj = new DapperDataAccess("SampleDB");
        //
        // GET: /DapperTest/
        public ActionResult Index()
        {
            //IEnumerable<Employee> emps = dapperobj.ExecuteReader<Employee>("spGetAllEmployees1", null);
          

            List<ParamterTemplate> parms = new List<ParamterTemplate>();
            parms.Add(new ParamterTemplate("EmpId", "1",typeof(System.Int32),"Input"));
            parms.Add(new ParamterTemplate("RecordCount", "0", typeof(System.Int32), "Output"));

            List<object> returnobj = new List<object>();

           
            IEnumerable<Employee> emps = dapperobj.ExecuteReader<Employee,ParamterTemplate>("GetEmp", parms, out returnobj, "SP");

            ViewBag.EmpCount = ((dynamic)returnobj[0]).RecordCount;
            //ViewBag.EmpCount = returnobj.FirstOrDefault();

            //ViewBag.EmpCount = dapperobj.ExecuteScalar<int>("select count(*) from Employee", null);
            return View(emps);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {

                //var param = new { EmpId = employee.EmpId, 
                //                  EmpName= employee.EmpName,
                //                  EmpAlias1 = employee.EmpAlias1,
                //                  EmpAlias2 = employee.EmpAlias2,
                //                  EmpAlias3 = employee.EmpAlias3,
                //                  ErrorCode = 0
                //};

                //dapperobj.ExecuteNonQuery("usp_Insert_Employee", param,"SP");

                List<object> returnobj = new List<object>();
                List<ParamterTemplate> parms = new List<ParamterTemplate>();
                parms.Add(new ParamterTemplate("EmpId",employee.EmpId, typeof(System.Int32), "Input"));
                parms.Add(new ParamterTemplate("EmpName", employee.EmpName, typeof(System.String), "Input"));
                parms.Add(new ParamterTemplate("EmpAlias1", employee.EmpAlias1, typeof(System.String), "Input"));
                parms.Add(new ParamterTemplate("EmpAlias2", employee.EmpAlias2, typeof(System.String), "Input"));
                parms.Add(new ParamterTemplate("EmpAlias3", employee.EmpAlias3, typeof(System.String), "Input"));
                parms.Add(new ParamterTemplate("ReturnValue", "0", typeof(System.Int32), "Output"));
                parms.Add(new ParamterTemplate("ErrorCode", "0", typeof(System.Int32), "Output"));
                dapperobj.ExecuteNonQuery<ParamterTemplate>("usp_Insert_Employee_retvalue", parms, out returnobj,"SP");

               
                TempData["ReturnValue"] = returnobj[0].ToString();
                TempData["ErrorCode"] = returnobj[1].ToString();
                //ViewBag.ErrorCode = returnobj[1].ToString();

            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var param = new { empid = id };
            Employee emp = dapperobj.ExecuteReader<Employee>("spGetFilterEmployees", param, "SP").Single();

           
            return View(emp);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp)
        {

            //dapperobj.ExecuteNonQuery("update Employee set EmpAlias3 = '" + emp.EmpAlias3 + "' where EmpId = " + emp.EmpId, null);
            dapperobj.ExecuteNonQuery("update Employee set EmpAlias3 = @EmpAlias3 where EmpId = @EmpId", new { emp.EmpAlias3, emp.EmpId }, null);
            return RedirectToAction("Index");
        }
	}
}