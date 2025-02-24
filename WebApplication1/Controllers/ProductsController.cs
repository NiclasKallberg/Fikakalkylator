using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(WebApplication1Context context) : ControllerBase
    {


        private readonly WebApplication1Context _context = context;


        private static ProductDTO ProductToDTO(Product product) =>
           new()
           {
               Id = product.Id,
               Name = product.Name,
               Points = product.Points,
               CreatedDate = product.CreatedDate,
               ModifiedDate = product.ModifiedDate,
               DeletedDate = product.DeletedDate,
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




        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct()
        {
            return await _context.Products
                .Select(x => ProductToDTO(x))
                .ToListAsync();
        }













        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
        //{
        //    var product = await _context.Products.FindAsync(id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return ProductToDTO(product);
        //}





        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }



            if (!ModelState.IsValid)
            {
                return GetErrors();
            }





            if (productDTO.DeletedDate is not null)
                product.DeletedDate = productDTO.DeletedDate;


            else
            {
                product.Name = productDTO.Name;
                product.Points = productDTO.Points;



                product.ModifiedDate = (DateTime)productDTO.ModifiedDate!;



            }





            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProductExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }







        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDTO)
        {
            //MITT START

            if (!ModelState.IsValid)
            {
                return GetErrors();
            }



            Product product = new()
            {
                Name = productDTO.Name,
                Points = productDTO.Points,
                CreatedDate = (DateTime)productDTO.CreatedDate!,
                ModifiedDate = (DateTime)productDTO.CreatedDate!
            };
            //MITT SLUT







            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ProductToDTO(product));
        }




        // Används inte

        // DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(Guid id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }




        
    }
}
