using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Purchase
    {
        public Guid Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity {  get; set; }



        [Required]
        [Range(0, int.MaxValue)]
        public int TotalPoints { get; set; }

        [Required]
        public required DateTime CreatedDate { get; set; }


        public Guid UserId { get; set; }

        [Required]
        public required User User { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        public required Product Product { get; set; }
    }
}
