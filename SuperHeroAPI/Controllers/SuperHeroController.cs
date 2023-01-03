using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroContext _context;

        public SuperHeroController(SuperHeroContext context)
        {
               _context = context;
        }

        [HttpGet]
        public IEnumerable<SuperHero> GetSuperHeroes()
        {
            IQueryable<SuperHero> query = _context.SuperHeroes;
            return query.ToList();
        }


        [HttpPost]
        public async Task<ActionResult<SuperHero>> CreateSuperHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToArrayAsync());
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateSuperHero(SuperHero hero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(hero.Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToArrayAsync());

        }

        [HttpDelete("{id}")]
        
        public async Task<ActionResult<SuperHero>> DeleteSuperHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id    );
            if (dbHero == null)
                return BadRequest("Hero not found");
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
