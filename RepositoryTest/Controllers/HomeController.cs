using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.GenericAbstract;
using RepositoryTest.Models;

namespace RepositoryTest.Controllers
{
    public class HomeController : Controller
    {


        EfRepositoryBase<Employee,SampleEntities> efrepobj = new EfRepositoryBase<Employee,SampleEntities>();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            
            //efrepobj.ExecuteScalar("update [Employees] set EmpAlias3 = 'John' where EmpId = 1");

            IEnumerable<Employee> emps = efrepobj.ExecuteReader<Employee>("spGetAllEmployees1", null);
            var mods = emps;
            ViewBag.EmpCount = efrepobj.ExecuteScalar<int>("select count(*) from Employee", null);
            return View(mods);
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
                efrepobj.Insert(employee);
                efrepobj.Save();
                
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {

            Employee emp = efrepobj.ExecuteReader<Employee>("spGetFilterEmployees @p0", id).Single();
            return View(emp);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp)
        {
            string query = "update Employee set EmpAlias3 = '" + emp.EmpAlias3 + "' where EmpId = " + emp.EmpId;
            efrepobj.ExecuteNonQuery("update Employee set EmpAlias3 = '" + emp.EmpAlias3 + "' where EmpId = " + emp.EmpId);
            return RedirectToAction("Index");
        }
	}
}