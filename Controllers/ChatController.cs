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

            var Query= db.chats.Where(c=> c.ProductId==id && c.ReceiverId==userid).OrderBy(c=>c.Timestamp).ToList();
            return View(Query);
        }

        public ActionResult Enquiry()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Enquiry(int id, Chat chat)
        {
            try
            {
                SellerContextcs db = new SellerContextcs();
               
                var rcv= db.Products.Where(p=>p.ProductId==id).Select(p=>p.UserId).FirstOrDefault();

                int usr= (int)HttpContext.Session.GetInt32("UserID");

                chat.SenderID = usr;     
                chat.ReceiverId = rcv;
                chat.ProductId = id;
                chat.Timestamp = DateTime.Now;

                db.chats.Add(chat);
                db.SaveChanges();

                return RedirectToAction("Details", "User", new { id = chat.ProductId });

            }
            catch (Exception ex)
            {
                return View();
            }
        }

       
        // YourController.cs
/*
        [HttpPost]
        public IActionResult SendMessage(int userId, int productId, Chat chat)
        {
            try
            {
                SellerContextcs db = new SellerContextcs();

                var senderid = (int)HttpContext.Session.GetInt32("UserID");

                chat.SenderID = senderid;
                chat.ReceiverId= userId;
                chat.ProductId = productId;
                chat.Message = "Connected to seller";
                chat.Timestamp= DateTime.Now;
                db.chats.Add(chat);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the error
                // Return an error response
                return Json(new { success = false });
            }
        }*/

    }
}
