using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using project_demo.Models;
using sell_hub.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace sell_hub.Controllers
{
    public class UserController : Controller
    {
        
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string Type, string search, string Category, string? SubCategory, string Availability)
        {
            SellerContextcs db = new SellerContextcs();
            int userid = (int)HttpContext.Session.GetInt32("UserID");
            if (Category != null)
            {
                var id = int.Parse(Category);

                if (SubCategory != null)
                {
                    var subid = int.Parse(SubCategory);
                    if (search == null)
                    {
                        var product = db.Products.Where(p => p.SubCategoryId == subid && p.UserId!= userid).ToList();
                        return View(product);
                    }
                    else
                    {
                        var product = db.Products.Where(p => p.SubCategoryId == subid && p.UserId != userid && p.ProductName.StartsWith(search) || search == null).ToList();

                        return View(product);

                    }
                }
                if (search == null)
                {

                    var product = db.Products.Where(p => p.CategoryId == id && p.UserId != userid).ToList();
                    return View(product);
                }
                else
                {
                    var product = db.Products.Where(p => p.CategoryId == id && p.UserId != userid && p.ProductName.StartsWith(search) || search == null).ToList();

                    return View(product);

                }

            }
            if (Type == "Sell")
            {
                if (search == null)
                {
                    var product = db.Products.Where(p => p.Type == Type && p.UserId != userid).ToList();
                    return View(product);
                }
                else
                {
                    var product = db.Products.Where(p => p.Type == Type && p.UserId != userid && p.ProductName.StartsWith(search) || search == null).ToList();

                    return View(product);

                }



            }
            if (Type == "Rent")
            {
                if (search == null)
                {
                    var product = db.Products.Where(p => p.Type == Type && p.UserId != userid).ToList();
                    return View(product);
                }
                else
                {
                    var product = db.Products.Where(p => p.Type == Type && p.UserId != userid && p.ProductName.StartsWith(search) || search == null).ToList();

                    return View(product);

                }
            }
            if (Availability == "Available")
            {
                if (search == null)
                {
                    var product = db.Products.Where(p => p.Availability == Availability && p.UserId != userid).ToList();
                    return View(product);
                }
                else
                {
                    var product = db.Products.Where(p => p.Availability == Availability && p.UserId != userid && p.ProductName.StartsWith(search) || search == null).ToList();

                    return View(product);

                }

            }

            var productList = db.Products.Where(p=>p.UserId != userid).ToList();


            return View(productList);
        }
        public ActionResult GetCategory()

        {
            SellerContextcs db = new SellerContextcs();
            var categories = db.Categories.ToList();
            return Json(categories);
        }

        public ActionResult GetSubCategory(int categoryId)
        {
            SellerContextcs db = new SellerContextcs();
            var subCategories = db.Subcategories.Where(sub => sub.CategoryId == categoryId).ToList();
            return Json(subCategories);
        }

        public ActionResult AddProduct()
        {

            return View();

        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            SellerContextcs db = new SellerContextcs();
            product.UserId = (int)HttpContext.Session.GetInt32("UserID");
            db.Products.Add(product);
            db.SaveChanges();
            HttpContext.Session.SetInt32("NewProductID", product.ProductId);
            return RedirectToAction("MultipleFileUpload");

        }


        public ActionResult PostedAds()
        {
            SellerContextcs db = new SellerContextcs();

            var id = (int)HttpContext.Session.GetInt32("UserID");

            var Query = db.Products.Where(p => p.UserId == id).ToList();

            return View(Query);
        }

        public ActionResult Details(int id)
        {
            SellerContextcs db = new SellerContextcs();
            var Query = (from product in db.Products
                         join user in db.UserMasters on product.UserId equals user.UserId
                         join subcat in db.Subcategories on product.SubCategoryId equals subcat.SubcategoryId
                         join cat in db.Categories on product.CategoryId equals cat.CategoryId
                         where product.ProductId == id
                         select new DetailProduct
                         {
                             ProductId = product.ProductId,
                             UserId = product.UserId,
                             ProductName = product.ProductName,
                             ProductDescription = product.ProductDescription,
                             Price = product.Price,
                             Type = product.Type,
                             Availability = product.Availability,
                             PostedDate = product.PostedDate,
                             Name = user.FirstName + " " + user.LastName,
                             CategoryName = cat.CategoryName,
                             SubcategoryName = subcat.SubcategoryName
                         }).FirstOrDefault();


            return View(Query);
        }

        [HttpPost]
        public ActionResult ReportAd(int productid, int userid, Report report)
        {
            try
            {
                SellerContextcs db = new SellerContextcs();
                report.ProductId = productid;
                report.UserId = userid;

                db.Reports.Add(report);
                db.SaveChanges();

                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                // Log the error
                // Return an error response
                return Json(new { success = false });
            }

        }

        [HttpPost]
        public IActionResult AddToWishlist(int productId, Wishlist wishlist )
        {
            try
            {
                var id = (int)HttpContext.Session.GetInt32("UserID");
                SellerContextcs db = new SellerContextcs();
                wishlist.ProductId = productId;
                wishlist.UserId = id;
                db.Wishlists.Add(wishlist);
                db.SaveChanges();

                // Return a success response
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the error
                // Return an error response
                return Json(new { success = false });
            }

        }


        public ActionResult WishList()
        {
            SellerContextcs db = new SellerContextcs();
            var id = (int)HttpContext.Session.GetInt32("UserID");

            var Query = (from wish in db.Wishlists
                         join product in db.Products on wish.ProductId equals product.ProductId
                         where wish.UserId == id
                         select new DetailWishlist
                         {

                             WishlistId = wish.WishlistId,
                             ProductId = product.ProductId,

                             ProductName = product.ProductName,

                             Price = product.Price,

                             Availability = product.Availability,

                         }).ToList();


            return View(Query);
        }
        public ActionResult WishDetails(int id)
        {
            SellerContextcs db = new SellerContextcs();
            var Query = (from product in db.Products
                         join user in db.UserMasters on product.UserId equals user.UserId
                         join subcat in db.Subcategories on product.SubCategoryId equals subcat.SubcategoryId
                         join cat in db.Categories on product.CategoryId equals cat.CategoryId
                         where product.ProductId == id
                         select new DetailProduct
                         {
                             ProductId = product.ProductId,
                             UserId = user.UserId,
                             ProductName = product.ProductName,
                             ProductDescription = product.ProductDescription,
                             Price = product.Price,
                             Type = product.Type,
                             Availability = product.Availability,
                             PostedDate = product.PostedDate,
                             Name = user.FirstName + " " + user.LastName,
                             CategoryName = cat.CategoryName,
                             SubcategoryName = subcat.SubcategoryName
                         }).FirstOrDefault();


            return View(Query);
        }

        public ActionResult RemoveWishlistItem(int id)
        {
            SellerContextcs db = new SellerContextcs();
            var query= db.Wishlists.Where(w=>w.WishlistId==id).FirstOrDefault();

            db.Wishlists.Remove(query);
            db.SaveChanges();

            return RedirectToAction("WishList");
        }

        public ActionResult MultipleFileUpload(int id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult MultipleFileUpload(int id, IEnumerable<IFormFile> GalleryPath)
        {
            SellerContextcs db = new SellerContextcs();
            

            foreach (var file in GalleryPath)
            {
                ProductImage img = new ProductImage();
                if (file != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream); // Use 'file' instead of 'GalleryPath'
                    }

                    img.ImagePath = "/images/" + fileName;

                    if (id == 0)
                    {
                        var productid = (int)HttpContext.Session.GetInt32("NewProductID");
                        img.ProductId = productid;
                    }
                    else
                    {
                        img.ProductId = id;
                    }

                    db.productImages.Add(img);
                }
            }

            // Move db.SaveChanges() outside the loop to improve performance
            db.SaveChanges();

            return RedirectToAction("PostedAds");
        }

        public ActionResult DisplayImages(int id)
        {
            SellerContextcs db = new SellerContextcs();
            var Query= db.productImages.Where(img=>img.ProductId==id).ToList();
            return View(Query);
        }

        public ActionResult SellersDetail(int id)
        {
            SellerContextcs db = new SellerContextcs();
            var Query = (from product in db.Products
                         join user in db.UserMasters on product.UserId equals user.UserId
                         join subcat in db.Subcategories on product.SubCategoryId equals subcat.SubcategoryId
                         join cat in db.Categories on product.CategoryId equals cat.CategoryId
                         where product.ProductId == id
                         select new DetailProduct
                         {
                             ProductId = product.ProductId,
                             UserId = product.UserId,
                             ProductName = product.ProductName,
                             ProductDescription = product.ProductDescription,
                             Price = product.Price,
                             Type = product.Type,
                             Availability = product.Availability,
                             PostedDate = product.PostedDate,
                             Name = user.FirstName + " " + user.LastName,
                             CategoryName = cat.CategoryName,
                             SubcategoryName = subcat.SubcategoryName
                         }).FirstOrDefault();


            return View(Query);

        }





    }
}
