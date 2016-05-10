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
       
        //
        // GET: /Home/
        public ActionResult Index()
        {
            EfRepositoryBase efrepobj = new EfRepositoryBase(new SampleEntities());
            //efrepobj.ExecuteScalar("update [Employees] set EmpAlias3 = 'John' where EmpId = 1");

            IEnumerable<Employee> emps = efrepobj.ExecuteReader<Employee>("spGetAllEmployees1", null);
            var mods = emps;
            return View(mods);
        }
	}
}