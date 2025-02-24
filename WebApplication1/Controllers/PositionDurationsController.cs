using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionDurationsController(WebApplication1Context context) : ControllerBase
    {


        //Mitt start
        private static PositionDurationDTO PositionDurationToDTO(PositionDuration positionDuration) =>
            new()
            {
                Id = positionDuration.Id,
                Position = positionDuration.Position,
                PositionStartDate = positionDuration.PositionStartDate,
                PositionEndDate = positionDuration.PositionEndDate,
                UserId = positionDuration.UserId,
                User = positionDuration.User
            };




        ObjectResult GetErrors()
        {

            //Reverse används för att visa felmeddelanden i rätt ordning
            string Errors = string.Join("\n", ModelState.Values.Reverse()
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));

            return StatusCode(StatusCodes.Status400BadRequest, Errors);
        }
        //Mitt slut





        private readonly WebApplication1Context _context = context;

        // GET: api/PositionDurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionDurationDTO>>> GetPositionDuration()
        {
            return await _context.PositionDurations
                .Where(x => x.PositionEndDate == null)
                .Select(x => PositionDurationToDTO(x))
                .ToListAsync();


        }




        // GET: api/PositionDurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDurationDTO>> GetPositionDuration(Guid id)
        {
            var positionDuration = await _context.PositionDurations.FindAsync(id);

            if (positionDuration == null)
            {
                return NotFound();
            }

            return PositionDurationToDTO(positionDuration);
        }







        // PUT: api/PositionDurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositionDuration(Guid id, PositionDurationDTO positionDurationDTO)
        {
            if (id != positionDurationDTO.Id)
            {
                return BadRequest();
            }

            var positionDuration = await _context.PositionDurations.FindAsync(id);
            if (positionDuration == null)
            {
                return NotFound();
            }








            if (!ModelState.IsValid)
            {
                return GetErrors();
            }



            positionDuration.Position = positionDurationDTO.Position;
            positionDuration.PositionStartDate = Convert.ToDateTime(positionDurationDTO.PositionStartDate);
            positionDuration.PositionEndDate = positionDurationDTO.PositionEndDate;
            positionDuration.UserId = positionDurationDTO.UserId;
            positionDuration.User = positionDurationDTO.User!;



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PositionDurationExists(id))
            {

                return NotFound();


            }



            return NoContent();
        }




        // POST: api/PositionDurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PositionDurationDTO>> PostPositionDuration(PositionDurationDTO positionDurationDTO)
        {
            //Mitt start

            if (!ModelState.IsValid)
            {
                return GetErrors();
            }



            var positionDuration = new PositionDuration
            {
                Position = positionDurationDTO.Position,
                PositionStartDate = Convert.ToDateTime(positionDurationDTO.PositionStartDate.ToString()),
                UserId = positionDurationDTO.UserId,
                User = _context.Users.Find(positionDurationDTO.UserId)!,

            };
            //Mitt slut





            _context.PositionDurations.Add(positionDuration);

            await _context.SaveChangesAsync();



            return CreatedAtAction(
                nameof(GetPositionDuration),
                new { id = positionDurationDTO.Id },
                positionDurationDTO);

        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePositionDuration(Guid id)
        //{
        //    var positionDuration = await _context.PositionDurations.FindAsync(id);
        //    if (positionDuration == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PositionDurations.Remove(positionDuration);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool PositionDurationExists(Guid id)
        {
            return _context.PositionDurations.Any(e => e.Id == id);
        }


        
    }
}
