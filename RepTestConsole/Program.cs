using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infosys.FoundationLibrary.DataAccess.GenericAbstract;
using RepositoryTest.Models;
using Infosys.FoundationLibrary.DataAccess.GenericInterface;
using System.Diagnostics;
using EntityFramework.BulkInsert.Extensions;
using System.Transactions;
using EntityFramework.Utilities;
namespace RepTestConsole
{


    class Program
    {
         private static List<rules> rulelist = new List<rules>
            {
                new rules()
                {
                    Code1 = "c1",Code2 = "c2"
                },
                new rules()
                {
                    Code1 = "c3",Code2="c4"
                }


            };

         private static List<claimlines> clmlst = new List<claimlines>
         {
              new claimlines()
             {
                 claimid = "cid-1",line="01",cptcode="ca"
             

             },
              new claimlines()
             {
                 claimid = "cid-1",line="04",cptcode="c1"
             },
             new claimlines()
             {
                 claimid = "cid-1",line="02",cptcode="c4"
             

             },
             new claimlines()
             {
                 claimid = "cid-1",line="03",cptcode="c2"
             }
            
             //new claimlines()
             //{
             //    claimid = "cid-1",line="02",cptcode="c1"
             //},
             //new claimlines()
             //{
             //    claimid = "cid-4",line="01",cptcode="c0"
             //},
             // new claimlines()
             //{
             //    claimid = "cid-4",line="02",cptcode="c4"
             //}
         };

        static void main(string[] args)
        {
            Console.WriteLine("input rules");
            foreach(var x in rulelist)
            {
                Console.WriteLine(x.Code1 +"\t" +  x.Code2);
            }
            Console.WriteLine("claim lines");
            foreach (var x in clmlst)
            {
                Console.WriteLine(x.claimid + "\t" + x.line + "\t" + x.cptcode);
            }

           
            var result = (from x in rulelist
                          from y in clmlst
                          where x.Code1 == y.cptcode || x.Code2 == y.cptcode
                          select new { x.Code1,x.Code2, y.claimid,y.line,y.cptcode }).ToList();
            Console.WriteLine("result");
            foreach(var res in result)
            {

                Console.WriteLine(res);
                Console.WriteLine();
            }

            var grupitems = result.GroupBy(x => new { x.Code1, x.Code2 }).ToList();

            var subgrups = grupitems.Where(g => g.Count()  > 1)
                .Select(x => new{a = x.Key,b= x.ToList(),c=string.Join<string>(",",x.ToList().Select(c => c.cptcode))});
            //string op="";
            foreach(var i in subgrups)
            {
              // op = string.Join<string>(",", i.b.Select(x => x.cptcode));
                var values = i.a.Code1 + "," + i.a.Code2 ;
                var values1 = i.a.Code2 + "," + i.a.Code1;
                Console.WriteLine(values + values1);
                Console.WriteLine(i.c);
                //var values = RuleOverridingModifier.Select(x => ";" + x + ";");
               if(i.c.Contains(values) || i.c.Contains(values1))
               {
                   i.b.ForEach(x => Console.WriteLine(x.claimid + x.Code1 + x.Code2 + x.cptcode + x.line));
               }
               // res = res.Where(r => values.Any(c => (";" + r.RuleOverridingmodifiers + ";").Contains(c)));
                
            }

           // Console.WriteLine(op);     
          


           
            

        }
        
    }
    class rules
    {
        public string Code1 { get; set; }
        public string Code2 { get; set; }
    }
    class claimlines
    {
        public string claimid { get; set; }
        public string line { get; set; }
        public string cptcode { get; set; }
    }
}
       