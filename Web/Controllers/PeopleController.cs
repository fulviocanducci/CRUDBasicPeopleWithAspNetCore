using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web.Contexts;
using Web.Models;
namespace Web.Controllers
{
   public class PeopleController : Controller
   {
      private readonly DatabaseContext _database;

      private IQueryable<People> GetPeopleQueryable()
      {
         return _database.Peoples.AsNoTrackingWithIdentityResolution();
      }

      private People GetPeopleById(int id)
      {
         return GetPeopleQueryable().FirstOrDefault(x => x.Id == id);
      }

      public PeopleController(DatabaseContext database)
      {
         _database = database;
      }

      public ActionResult Index()
      {
         return View(GetPeopleQueryable().ToList());
      }

      public ActionResult Details(int id)
      {
         People model = GetPeopleById(id);
         if (model != null)
         {
            return View(model);
         }
         return RedirectToAction(nameof(Index));
      }

      public ActionResult Create()
      {
         return View("CreateOrUpdate");
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create(People model)
      {
         try
         {
            if (ModelState.IsValid)
            {
               _database.Peoples.Add(model);
               _database.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View("CreateOrUpdate");
         }
      }

      public ActionResult Edit(int id)
      {
         People model = GetPeopleById(id);
         if (model != null)
         {
            return View("CreateOrUpdate", model);
         }
         return RedirectToAction(nameof(Index));
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(People model)
      {
         try
         {
            if (ModelState.IsValid)
            {
               _database.Peoples.Update(model);
               _database.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View("CreateOrUpdate");
         }
      }

      public ActionResult Delete(int id)
      {
         People model = GetPeopleById(id);
         if (model != null)
         {
            return View(model);
         }
         return RedirectToAction(nameof(Index));
      }

      [HttpPost]
      [ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteAction(int id)
      {
         try
         {
            People model = GetPeopleById(id);
            if (model != null)
            {
               _database.Peoples.Remove(model);
               _database.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }
   }
}
