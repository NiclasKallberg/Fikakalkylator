using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PositionDurationDTO
    {
        public Guid Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Position { get; set; }

        public DateTime? PositionStartDate { get; set; }
        public DateTime? PositionEndDate { get; set; }
        public Guid UserId { get; set; }

        public User? User { get; set; }
    }
}
