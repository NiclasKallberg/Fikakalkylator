namespace BlazorApp1.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
        public required int Points { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
