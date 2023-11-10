using Microsoft.AspNetCore.Mvc;
using sell_hub.Models;

namespace sell_hub.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult GetChats(int id)
        {
            SellerContextcs db = new SellerContextcs();

            var userid = (int)HttpContext.Session.GetInt32("UserID");

            var Query= db.chats.Where(c=> c.ProductId==id).OrderBy(c=>c.Timestamp).ToList();
            return View(Query);
        }
       
        // YourController.cs

        [HttpPost]
        public IActionResult SendMessage(int userId, int productId)
        {
            try
            {
                SellerContextcs db = new SellerContextcs();




                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the error
                // Return an error response
                return Json(new { success = false });
            }
        }

    }
}
