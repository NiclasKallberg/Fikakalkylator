using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        [StringLength(100, ErrorMessage = "Användarnamnet är för långt!")]
        public string? Username { get; set; }




        [Required(ErrorMessage = "Förnamn måste finnas med!")]
        [StringLength(40, ErrorMessage = "Förnamnet är för långt!")]
        public required string FirstName { get; set; }


        [Required(ErrorMessage = "Efternamn måste finnas med!")]
        [StringLength(40, ErrorMessage = "Efternamnet är för långt!")]
        public required string LastName { get; set; }




        public DateTime? CreatedDate { get; set; }



        public DateTime? ModifiedDate { get; set; }



        public DateTime? DeletedDate { get; set; }




    }
}
