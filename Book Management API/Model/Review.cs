using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Management_API.Model
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }     
        public string UserId { get; set; }
    }
}
