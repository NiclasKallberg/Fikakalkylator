using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(40)]
        public required string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public required int Points {  get; set; }

        [Required]
        public required DateTime CreatedDate { get; set; }

        [Required]
        public required DateTime ModifiedDate { get; set; }







        public DateTime? DeletedDate { get; set; }


    }
}
