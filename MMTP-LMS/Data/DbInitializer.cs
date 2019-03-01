using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            //if (context.Course.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //var persons = new Person[]
            //{
            //    new Person{FirstName="Jon", LastName="Steger", Email="Jon@Steger.se", CourseId=1, PasswordHash="Lexicon!0"},
            //    new Person{FirstName="Håkan", LastName="Rös", Email="Håkan@gmail.se", CourseId=1, PasswordHash="Lexicon!0"}
            //};
            //foreach (Person s in persons)
            //{
            //    context.Person.Add(s);
            //}
            //context.SaveChanges();

            //var courses = new Course[]
            //{
            //new Course{Name="c#", Description="Lern c#", StartDate=DateTime.Now, EndDate=DateTime.Now.AddYears(1) },
            //new Course{Name="Java", Description="Lern Java", StartDate=DateTime.Now, EndDate=DateTime.Now.AddYears(1) }

            //};
            //foreach (Course c in courses)
            //{
            //    context.Course.Add(c);
            //}
            //context.SaveChanges();
            //var modules = new Module[]
            //{
            //new Module{Name="c# Bacic", Description="Lern c# bacics", StartDate=DateTime.Now, EndDate=DateTime.Now.AddMonths(6), CourseId=3 },
            //new Module{Name="c# Fundementals", Description="Lern c# Fundementals", StartDate=DateTime.Now, EndDate=DateTime.Now.AddMonths(6), CourseId=3 },
            //new Module{Name="Java# Bacic", Description="Lern c#", StartDate=DateTime.Now, EndDate=DateTime.Now.AddMonths(6), CourseId=4 },
            //new Module{Name="Java# Fundementals", Description="Lern Java", StartDate=DateTime.Now, EndDate=DateTime.Now.AddMonths(6), CourseId=4 }

            //};
            //foreach (Module c in modules)
            //{
            //    context.Module.Add(c);
            //}

            //context.SaveChanges();
            //var lmsActivityTypes = new LmsActivityType[]
            //{
            //  new LmsActivityType{Name="E-Lerning"},
            //  new LmsActivityType{Name="Database"},
            //  new LmsActivityType{Name="Föreläsning"}
            //};
            //foreach(var c in lmsActivityTypes)
            //{
            //    lmsActivityTypes.Add(c);
            //}
            //context.SaveChanges();
           // var lmsActivitys = new LmsActivity[]
           //{
           // new LmsActivity{Name="c# Bacic", Description="Lern c# bacics mega", StartDate=DateTime.Now, EndTime=DateTime.Now, LmsActivityTypeId =1, ModuleId =7 },
           // new LmsActivity{Name="c# Bacic2", Description="Lern c# bacics mega2", StartDate=DateTime.Now, EndTime=DateTime.Now,LmsActivityTypeId =1, ModuleId =7 },

           //};
           // foreach (LmsActivity c in lmsActivitys)
           // {
           //     context.LmsActivity.Add(c);
           // }

           // context.SaveChanges();

        }
    }
}
