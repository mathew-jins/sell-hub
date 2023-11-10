using Microsoft.AspNetCore.Mvc;
using project_demo.Models;
using sell_hub.Models;

namespace sell_hub.Controllers
{
    public class AdminController : Controller
    {
        
        public ActionResult Index()
        {
            SellerContextcs db = new SellerContextcs();
            var categoryList = db.Categories.ToList();
            return View(categoryList);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category category)
        {
            SellerContextcs db = new SellerContextcs();
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddSubCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSubCategory(Subcategory subcategory, int id)
        {
            SellerContextcs db = new SellerContextcs();

            subcategory.CategoryId = id;

            db.Subcategories.Add(subcategory);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ReportedAds()
        {
            SellerContextcs db = new SellerContextcs();

            var Query = (from report in db.Reports
                         join product in db.Products on report.ProductId equals product.ProductId
                         join user in db.UserMasters on report.UserId equals user.UserId
                         select new ReportedAd
                         {
                             ProductId = product.ProductId,
                             ProductName = product.ProductName,
                             Price = product.Price,
                             Username = user.UserName,
                             Name = user.FirstName + " " + user.LastName,
                             Number = user.Phone

                         }).ToList();


            return View(Query);
        }

        public ActionResult Details(int id)
        {
            SellerContextcs db = new SellerContextcs();
            var Query = (from result in db.Reports
                         join product in db.Products on result.ProductId equals product.ProductId
                         join user in db.UserMasters on product.UserId equals user.UserId
                         join subcat in db.Subcategories on product.SubCategoryId equals subcat.SubcategoryId
                         join cat in db.Categories on product.CategoryId equals cat.CategoryId
                         where product.ProductId == id
                         select new DetailReport
                         {
                             ReportId = result.ReportId,
                             ProductId = product.ProductId,
                             ProductName = product.ProductName,
                             ProductDescription = product.ProductDescription,
                             Price = product.Price,
                             Type = product.Type,
                             Availability = product.Availability,
                             PostedDate = product.PostedDate,
                             CategoryName = cat.CategoryName,
                             SubCategoryName = subcat.SubcategoryName,
                             UserID = user.UserId,
                             UserName = user.UserName,
                             Name = user.FirstName + " " + user.LastName,
                             Email = user.Email,
                             Phone = user.Phone,
                             Address = user.Address

                         }).FirstOrDefault();

            return View(Query);
        }

        public ActionResult RemoveAd(int id)
        {
            SellerContextcs db = new SellerContextcs();
            var Query = db.Products.Find(id);
            var report = db.Reports.Where(r => r.ProductId == id).ToList();

            db.Reports.RemoveRange(report);
            db.Products.Remove(Query);

            db.SaveChanges();

            return RedirectToAction("ReportedAds");
        }

        public ActionResult Delete(int id)
        {
            SellerContextcs db = new SellerContextcs();
            var Query = db.Reports.Find(id);
            db.Reports.Remove(Query);
            db.SaveChanges();
            return RedirectToAction("ReportedAds");
        }

    }
}
