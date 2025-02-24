using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class ProductDTO
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Namn på vara måste finnas med!")]
        [StringLength(40, ErrorMessage = "Namn på vara är för långt!")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Poäng måste finnas med!")]
        [Range(0, int.MaxValue,ErrorMessage = "Poängen måste vara ett positivt tal!")]
        public required int Points { get; set; }


        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
