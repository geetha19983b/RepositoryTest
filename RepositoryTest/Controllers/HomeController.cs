using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infosys.FoundationLibrary.DataAccess.GenericAbstract;
using RepositoryTest.Models;
using Infosys.FoundationLibrary.DataAccess.GenericInterface;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;

namespace RepositoryTest.Controllers
{
    public class HomeController : Controller
    {

        private static IDbFactory<SampleEntities> dbFactory = new DbFactory<SampleEntities>();
        private readonly IUnitOfWork<SampleEntities> unitOfWork = new UnitOfWork<SampleEntities>(dbFactory);
        //IGenericDataAccess efrepobj = new EfDataAccess<SampleEntities>(dbFactory);

        //public HomeController(IUnitOfWork unitOfWork)
        //{
        //    this.unitOfWork = unitOfWork;
        //}
        //
        // GET: /Home/
        public ActionResult Index()
        {

            //efrepobj.ExecuteScalar("update [Employees] set EmpAlias3 = 'John' where EmpId = 1");

            IEnumerable<Employee> emps = unitOfWork.EfRepDA.ExecuteReader<Employee>("spGetAllEmployees1", null);
            // emps = unitOfWork.EfRepDA.ExecuteReader<Employee>("GetEmp {0}", 1);
            //var result = unitOfWork.EfRepDA.GetAll<GetEmp_Result>().ToList();

            var mods = emps;
            ViewBag.EmpCount = unitOfWork.EfRepDA.ExecuteScalar<int>("select count(*) from Employee", null);
            return View(mods);
        }
        public ActionResult SP_Mul()
        {

            ObjectParameter recordCount = new ObjectParameter("RecordCount", typeof(int?));
            var result = unitOfWork.EfRepDA.DbContext.GetEmp(1, recordCount).ToList();

           
            
            ViewBag.EmpCount = recordCount.Value;
            return View(result);
            

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
                unitOfWork.EfRepDA.Insert<Employee>(employee);
                //efrepobj.Save();
                unitOfWork.Commit();

            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {

            Employee emp = unitOfWork.EfRepDA.ExecuteReader<Employee>("spGetFilterEmployees @p0", id).Single();
            return View(emp);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp)
        {
            string query = "update Employee set EmpAlias3 = '" + emp.EmpAlias3 + "' where EmpId = " + emp.EmpId;
            unitOfWork.EfRepDA.ExecuteNonQuery("update Employee set EmpAlias3 = '" + emp.EmpAlias3 + "' where EmpId = " + emp.EmpId, null);
            return RedirectToAction("Index");
        }
    }
}