using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models
{


    public class User
    {

        public int Position { get; set; } = 0;

        public int TotalPoints { get; set; }


        public string OrderByThisName
        {

            get
            {
                return Username ?? LastName;

            }

        }



        public string DisplayName
        {

            get
            {

                return Username ?? FirstName + " " + LastName;
            }

        }



        public Guid Id { get; set; }

        public string? Username { get; set; }





        public required string FirstName { get; set; }


        public required string LastName { get; set; }




        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

    }
}