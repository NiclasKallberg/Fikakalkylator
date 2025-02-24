using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(WebApplication1Context context) : ControllerBase
    {

        private readonly WebApplication1Context _context = context;


        private static UserDTO UserToDTO(User user) =>
            new()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.ModifiedDate
            };




        //Skicka felmeddelanden till klienten om ModelState inte stämmer
        ObjectResult GetErrors()
        {
            
            //Reverse används för att visa felmeddelanden i rätt ordning
            string Errors = string.Join("\n", ModelState.Values.Reverse()
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));

            return StatusCode(StatusCodes.Status400BadRequest, Errors);
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser()
        {
            //Returnera allt som inte har DeletedDate
            return await _context.Users
            .Where(x => x.DeletedDate == null)
            .Select(x => UserToDTO(x))
            .ToListAsync();
        }



        






        // Används inte
        /*******************************/
        //// GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return UserToDTO(user);
        //}









        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }







            //Min kod början
            /*************************************/

            if (!ModelState.IsValid)
            {
                return GetErrors();
            }




            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;

            //Om DTO-klassen har DeletedDate betyder det att det ska tas bort,
            //annars ska användaren bara redigeras
            if (userDTO.DeletedDate is not null)
                user.DeletedDate = userDTO.DeletedDate;
                


            else
            {
                //Om användarnamn från klient är en tom sträng,
                //gör den null för att få bort varning
                if (userDTO.Username?.Trim()=="")
                    userDTO.Username = null;

                user.Username = userDTO.Username;



                user.ModifiedDate = Convert.ToDateTime(userDTO.ModifiedDate.ToString());

            }

            //Min kod slut





            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_context.Users.Any(e => e.Id == id))
            {
                return NotFound();
            }

            return NoContent();
        }









        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDTO)
        {

            //Min kod början
            /********************************/

            if (!ModelState.IsValid)
            {
                return GetErrors();
            }




            //Om användarnamn från klient är en tom sträng,
            //gör om detta till null
            if (userDTO.Username?.Trim()=="")
                userDTO.Username = null;


            User user = new()
            {
                Username = userDTO.Username,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                CreatedDate = Convert.ToDateTime(userDTO.CreatedDate.ToString()),
                ModifiedDate = Convert.ToDateTime(userDTO.CreatedDate.ToString())

            };
            //Min kod slut







            _context.Users.Add(user);
            await _context.SaveChangesAsync();



            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.Id },
                UserToDTO(user));


        }




        //Används inte
        /*****************************/

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(Guid id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
