using System.ComponentModel.DataAnnotations;

namespace sell_hub.Models
{
    public class Chat
    {
        [Key]
        public int ChatId {  get; set; }
        public int SenderID {  get; set; }
        public int ReceiverId {  get; set; }
        public int ProductId {  get; set; }
        public string Message {  get; set; }
        public DateTime Timestamp {  get; set; }
    }
}
