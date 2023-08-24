using System.ComponentModel.DataAnnotations;

namespace Product___Reviews.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
        
    }

    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

    }
}
