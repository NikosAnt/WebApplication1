using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzasController(AppDbContext context)
        {
            _context = context;
        }

        // GET /pizzas
        [HttpGet]
        public ActionResult<IEnumerable<Pizza>> GetAll()
        {
            return _context.Pizzas.ToList();
        }

        // GET /pizzas/{name}
        [HttpGet("{name}")]
        public ActionResult<Pizza> Get(string name)
        {
            var pizza = _context.Pizzas.FirstOrDefault(p => p.Name == name);
            if (pizza == null) return NotFound();
            return pizza;
        }

        // POST /pizzas
        [HttpPost]
        public ActionResult<Pizza> Post([FromBody] Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { name = pizza.Name }, pizza);
        }

        // DELETE /pizzas/{name}
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            var pizza = _context.Pizzas.FirstOrDefault(p => p.Name == name);
            if (pizza == null) return NotFound();
            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
            return NoContent();
        }

        // PUT /pizzas
        [HttpPut]
        public IActionResult Update([FromBody] Pizza pizza)
        {
            var existing = _context.Pizzas.FirstOrDefault(p => p.Name ==  pizza.Name);
            if (existing == null) return NotFound();
            existing.Amount = pizza.Amount;
            existing.Price = pizza.Price;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
