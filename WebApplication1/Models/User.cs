using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        public Guid Id { get; set; }


        [StringLength(100)]
        public string? Username { get; set; }


        [Required]
        [StringLength(40)]
        public required string FirstName { get; set; }


        [Required]
        [StringLength(40)]
        public required string LastName { get; set; }





        [Required]
        public required DateTime CreatedDate { get; set; }



        [Required]
        public required DateTime ModifiedDate { get; set; }





        /*När användare raderas i GUI:n så läggs det till en tid här
        och den slutar då visas i GUI:n men finns fortfarande i databasen*/
        public DateTime? DeletedDate { get; set; }





    }
}
