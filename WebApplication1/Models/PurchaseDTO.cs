using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class PurchaseDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Antal måste finnas med!")]
        [Range(0, int.MaxValue, ErrorMessage = "Antalet måste vara ett positivt tal!")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Den totala poängen måste finnas med!")]
        [Range(0, int.MaxValue, ErrorMessage = "Den totala poängen måste vara ett positivt tal!")]
        public int TotalPoints { get; set; }


        public DateTime? CreatedDate { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }



    }
}
