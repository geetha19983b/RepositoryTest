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
        
        static void Main(string[] args)
        {
            //EFBulkInsert();
            EFUtilities();


        }
        private static List<Student> CreateStudents()
        {
            var students = new List<Student>();
            for (int i = 0; i < 1000000; i++)
            {
                var student = new Student {StudentId = i, Name = Guid.NewGuid().ToString() };
                students.Add(student);
            }
            return students;
        }
        private static void EFUtilities()
        {
            IDbFactory<SampleEntities> dbFactory = new DbFactory<SampleEntities>();
            IUnitOfWork<SampleEntities> unitOfWork = new UnitOfWork<SampleEntities>(dbFactory);
            Stopwatch sw = new Stopwatch();
            var students = CreateStudents();



            sw.Restart();
            using (var ctx = unitOfWork.EfRepDA.DbContext)
            {
                using (var transactionScope = new TransactionScope())
                {
                    EFBatchOperation.For(ctx, ctx.Students).InsertAll(students);
                    sw.Stop();

                    Console.WriteLine(
                        "Added 100000 entities in {0}", sw.Elapsed.ToString());

                    sw.Restart();
                    //int recrdinstrd = ctx.SaveChanges();
                    transactionScope.Complete();
                    sw.Stop();

                   // Console.WriteLine(
                    //     "Saved {0} entities in {1}", recrdinstrd, sw.Elapsed.ToString());
                }
            }
        }
        private static void EFBulkInsert()
        {
            try
            {

                IDbFactory<SampleEntities> dbFactory = new DbFactory<SampleEntities>();
                IUnitOfWork<SampleEntities> unitOfWork = new UnitOfWork<SampleEntities>(dbFactory);
                Stopwatch sw = new Stopwatch();
                var students = CreateStudents();



                sw.Restart();
                //AddRange rulez, no need for db.Configuration.AutoDetectChangesEnabled = false;
                using (var ctx = unitOfWork.EfRepDA.DbContext)
                {
                    using (var transactionScope = new TransactionScope())
                    {
                        var options = new BulkInsertOptions
                        {
                            EnableStreaming = true,
                        };
                        ctx.BulkInsert(students, options);

                        // db.Students.AddRange(students);
                        sw.Stop();

                        Console.WriteLine(
                            "Added 100000 entities in {0}", sw.Elapsed.ToString());

                        sw.Restart();
                        int recrdinstrd = ctx.SaveChanges();
                        transactionScope.Complete();
                        sw.Stop();

                        Console.WriteLine(
                             "Saved {0} entities in {1}", recrdinstrd, sw.Elapsed.ToString());
                        
                    }
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        private static void AddRange()
        {
              try
            {
              
                IDbFactory<SampleEntities> dbFactory = new DbFactory<SampleEntities>();
                IUnitOfWork<SampleEntities> unitOfWork = new UnitOfWork<SampleEntities>(dbFactory);
                Stopwatch sw = new Stopwatch();
                var students = CreateStudents();


                
                sw.Restart();
                //AddRange rulez, no need for db.Configuration.AutoDetectChangesEnabled = false;
                unitOfWork.EfRepDA.DbContext.Configuration.AutoDetectChangesEnabled = false;
                unitOfWork.EfRepDA.DbContext.Configuration.ValidateOnSaveEnabled = false;
                unitOfWork.EfRepDA.DbContext.Students.AddRange(students);

                // db.Students.AddRange(students);
                sw.Stop();

                Console.WriteLine(
                    "Added 100000 entities in {0}", sw.Elapsed.ToString());

                sw.Restart();
                int recrdinstrd = unitOfWork.EfRepDA.DbContext.SaveChanges();
                sw.Stop();

                Console.WriteLine(
                     "Saved {0} entities in {1}", recrdinstrd, sw.Elapsed.ToString());

                unitOfWork.EfRepDA.DbContext.Configuration.AutoDetectChangesEnabled = true;
                unitOfWork.EfRepDA.DbContext.Configuration.ValidateOnSaveEnabled = true;
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

        }
    }
}
