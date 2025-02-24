using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController(WebApplication1Context context) : ControllerBase
    {
        private readonly WebApplication1Context _context = context;



        // Mitt start
        private static PurchaseDTO PurchaseToDTO(Purchase purchase) =>
            new()
            {
                Id = purchase.Id,
                TotalPoints = purchase.TotalPoints,
                Quantity = purchase.Quantity,
                CreatedDate = purchase.CreatedDate,
                UserId = purchase.UserId,
                ProductId = purchase.ProductId
            };




        ObjectResult GetErrors()
        {

            //Reverse används för att visa felmeddelanden i rätt ordning
            string Errors = string.Join("\n", ModelState.Values.Reverse()
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));

            return StatusCode(StatusCodes.Status400BadRequest, Errors);
        }

        // Mitt slut




        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetPurchase()
        {
            //Köp och poäng nollställs vid årsskiftet så därför hämtas bara nuvarande års köp till klienten
            return await _context.Purchases
                .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
                .Select(x => PurchaseToDTO(x))
                .ToListAsync();
        }


        
        //Denna kan användas om man vill hämta precis alla köp som finns i databasen
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetPurchaseAll()
        {

            return await _context.Purchases
                .Select(x => PurchaseToDTO(x))
                .ToListAsync();
        }






        // Används inte
        /*************************************/

        // GET: api/Purchases/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<PurchaseDTO>> GetPurchase(Guid id)
        //{
        //    var purchase = await _context.Purchases.FindAsync(id);

        //    if (purchase == null)
        //    {
        //        return NotFound();
        //    }

        //    return PurchaseToDTO(purchase);
        //}






        // Används inte
        /*************************************************/
       
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPurchase(Guid id, PurchaseDTO purchaseDTO)
        //{
        //    if (id != purchaseDTO.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var purchase = await _context.Purchases.FindAsync(id);

        //    if (purchase == null)
        //    {
        //        return NotFound();
        //    }


        //    purchase.Quantity = purchaseDTO.Quantity;
        //    purchase.CreatedDate = purchaseDTO.CreatedDate;
        //    purchase.UserId = purchaseDTO.UserId;
        //    purchase.User = purchaseDTO.User;
        //    purchase.ProductId = purchaseDTO.ProductId;
        //    purchase.Product = purchaseDTO.Product;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException) when (!PurchaseExists(id))
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}




        [HttpPost]
        public async Task<ActionResult<PurchaseDTO>> PostPurchase(PurchaseDTO purchaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return GetErrors();
            }

            var purchase = new Purchase
            {
                Quantity = purchaseDTO.Quantity,
                TotalPoints = _context.Products.Find(purchaseDTO.ProductId)!.Points * purchaseDTO.Quantity,
                CreatedDate = Convert.ToDateTime(purchaseDTO.CreatedDate.ToString()),
                UserId = purchaseDTO.UserId,
                User = _context.Users.Find(purchaseDTO.UserId)!,
                ProductId = purchaseDTO.ProductId,
                Product = _context.Products.Find(purchaseDTO.ProductId)!,
            };

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Id }, PurchaseToDTO(purchase));
        }





        //Detta används i nuläget bara för att ångra ett köp (länk hos klienten som visas i ett par sekunder efter man registrerat ett köp)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(Guid id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }



            if (!ModelState.IsValid)
            {
                return GetErrors();
            }




            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseExists(Guid id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }
    }
}
