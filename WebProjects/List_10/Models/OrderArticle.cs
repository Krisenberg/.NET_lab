using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace List_10.Models
{
    public class OrderArticle
    {
        public int Id { get; set; }

        [Required]
        public int ArticleHistoryId { get; set; }

        [ForeignKey("ArticleHistoryId")]
        public ArticleHistory ArticleHistory { get; set; }

        public int Quantity { get; set; }

        public double Value { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public OrderArticle() { }

        public OrderArticle(int articleHistoryId, int quantity, double value, int orderId)
        {
            ArticleHistoryId = articleHistoryId;
            Quantity = quantity;
            Value = value;
            OrderId = orderId;
        }
    }
}
