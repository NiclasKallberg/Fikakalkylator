using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models
{
    public class PositionDuration
    {
        public Guid Id { get; set; }

        public required int Position { get; set; }

        public required DateTime PositionStartDate { get; set; }

        public DateTime? PositionEndDate { get; set; }

        public Guid UserId { get; set; }
    }
}
