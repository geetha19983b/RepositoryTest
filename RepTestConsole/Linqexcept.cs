using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepTestConsole
{
    public class Linqexcept
    {
        static void Main(string[] args)
        {
            //var res = from x in clmlst
            //          select new { id = x.claimid, jn = string.Join(",",  x.cptcode , x.line) };
            //foreach(var i in res)
            //    Console.WriteLine(i);

            var res = (from row in clmlsthsrt
                       let cptharray = row.cptcode.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                       where clmlst.Any(x => x.cptcode.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Intersect(cptharray).Any())
                       select row).ToList();
            foreach (var i in res)
                Console.WriteLine(i.claimid + i.cptcode + i.line);
        }

        private static List<claimlines1> clmlst = new List<claimlines1>
         {
              new claimlines1()
             {
                 claimid = "cid-1",line="01",cptcode="ca;c1;c2"
             

             },
              new claimlines1()
             {
                 claimid = "cid-1",line="04",cptcode="c1;c4;c5;"
             },
             new claimlines1()
             {
                 claimid = "cid-1",line="02",cptcode=" "
             

             },
              new claimlines1()
             {
                 claimid = "cid-1",line="02",cptcode="c6;c7"
             

             },
             new claimlines1()
             {
                 claimid = "cid-1",line="03",cptcode="c2"
             }
         };
        private static List<claimlines1> clmlsthsrt = new List<claimlines1>
         {
              new claimlines1()
             {
                 claimid = "cid-1",line="01",cptcode="c10;c;11"
             

             },
              new claimlines1()
             {
                 claimid = "cid-1",line="04",cptcode="c11;c14;c15;"
             },
             new claimlines1()
             {
                 claimid = "cid-1",line="02",cptcode="c41"
             

             },
             new claimlines1()
             {
                 claimid = "cid-1",line="03",cptcode="c21;c23"
             }
         };
       
    }
    class claimlines1
    {
        public string claimid { get; set; }
        public string line { get; set; }
        public string cptcode { get; set; }
    }
   
}
