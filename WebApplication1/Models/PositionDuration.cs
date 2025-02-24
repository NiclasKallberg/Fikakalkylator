using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PositionDuration
    {
        public Guid Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Position { get; set; }

        [Required]
        public required DateTime PositionStartDate { get; set; }

        public DateTime? PositionEndDate { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public required User User { get; set; }

    }
}
