namespace BlazorApp1.Models
{
    public class Purchase
    {
        public Guid Id { get; set; }

        public required int Quantity { get; set; }

        public int TotalPoints { get; set; }

        public required DateTime CreatedDate { get; set; }

        public required Guid UserId { get; set; }

        public required Guid ProductId { get; set; }
    }
}
