using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task <ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task <ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(hero.Id);
            if (hero == null)
            {
                return BadRequest("Hero Not Found");
            }

            dbhero.Name = hero.Name;
            dbhero.firstName = hero.firstName;
            dbhero.lastName = hero.lastName;
            dbhero.place = hero.place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]     
        public async Task <ActionResult<List<SuperHero>>> DeleteSuperHero(int Id)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(Id);
            if ( dbhero == null )
            {
                return BadRequest("Hero Not Found");
            }

            _context.SuperHeroes.Remove(dbhero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
