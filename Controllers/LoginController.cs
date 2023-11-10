using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using project_demo.Models;
using sell_hub.Models;

namespace sell_hub.Controllers
{
    public class LoginController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            SellerContextcs db = new SellerContextcs();
            var users = db.UserMasters.ToList();
            return View(users);
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(UserMaster user)
        {
            try
            {
                SellerContextcs db = new SellerContextcs();

                if (user.ProfilePicture != null)
                {
                    // Generate a unique filename for the profile picture
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(user.ProfilePicture.FileName);

                    // Combine the web root path with the "profile_pictures" folder
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile_pictures");

                    // Combine the folder path with the generated filename
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await user.ProfilePicture.CopyToAsync(stream);
                    }

                    // Set the user's profile picture path
                    user.ProfilePath = "/profile_pictures/" + fileName;
                }

                user.Status = 0;
                user.Role = "user";
               

                db.UserMasters.Add(user);
                db.SaveChanges();

                return RedirectToAction("Signin");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signin(UserMaster user)
        {
            SellerContextcs db = new SellerContextcs();


            var Query = db.UserMasters.Where(u => u.UserName == user.UserName && u.Password == user.Password && u.Status == 1).FirstOrDefault();
            if (Query != null)
            {
                HttpContext.Session.SetInt32("UserID", Query.UserId);

                if (Query.Role == "admin")
                {

                    return RedirectToAction("Index", "Admin");
                }
                else if (Query.Role == "user")
                {
                    return RedirectToAction("Index", "User");

                }
                return RedirectToAction("Index");
            }


            return View();

        }
    }
}
