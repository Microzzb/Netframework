using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Netframework.Data;
using Netframework.Models;
using Netframework.ViewModels;
using PagedList;

namespace Netframework.Controllers
{
    public class MusicsController : Controller
    {
        private NetframeworkContext db = new NetframeworkContext();

        // GET: Musics
        public ActionResult Index(string category, string search, int? page, string sortBy)
        {
            MusicIndexViewModel viewModel = new MusicIndexViewModel();
            var musics = db.Musics.Include(m => m.Category);
     
            if (!String.IsNullOrEmpty(category))
            {
                musics = musics.Where(m => m.Category.Name == category);
            }
            if (!String.IsNullOrEmpty(search))
            {
                musics = musics.Where(m => m.Name.Contains(search) ||
                m.Description.Contains(search) ||
                m.Category.Name.Contains(search));
            
                viewModel.Search = search;
            }
            viewModel.CatsWithCount = from matchingMusics in musics
                                      where matchingMusics.CategoryID != null
                                      group matchingMusics by
                                      matchingMusics.Category.Name into catGroup
                                      select new CategoryWithCount()
                                      {
                                          CategoryName = catGroup.Key,
                                          MusicCount = catGroup.Count()
                                      };

            if (!String.IsNullOrEmpty(category))
            {
                musics = musics.Where(m => m.Category.Name == category);
                viewModel.Category = category;
            }
            
            switch (sortBy)
            {
                case "price_lowest":
                    musics = musics.OrderBy(m =>m.Price);
                    break;
                case "price_highest":
                    musics = musics.OrderByDescending(m => m.Price);
                    break;
                default:
                    musics = musics.OrderBy(m => m.Name);
                    break;
                    
            }
            const int PageItems = 3;
            int currentPage = (page ?? 1);
            viewModel.Musics = musics.ToPagedList(currentPage, PageItems);
            viewModel.SortBy = sortBy;
            viewModel.Sorts = new Dictionary<string, string>
            {
                { "Price low to high", "price_lowest"},
                { "Price high to low", "price_highest"}
            };
            return View(viewModel);
        }

        // GET: Musics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // GET: Musics/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Musics/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Description,CategoryID")] Music music)
        {
            if (ModelState.IsValid)
            {
                db.Musics.Add(music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", music.CategoryID);
            return View(music);
        }

        // GET: Musics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", music.CategoryID);
            return View(music);
        }

        // POST: Musics/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Description,CategoryID")] Music music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", music.CategoryID);
            return View(music);
        }

        // GET: Musics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Music music = db.Musics.Find(id);
            db.Musics.Remove(music);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
