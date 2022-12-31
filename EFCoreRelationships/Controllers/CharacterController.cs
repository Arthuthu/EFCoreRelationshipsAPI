global using EFCoreRelationships.Data;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;

        public CharacterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get(int userId)
        {

            var characters = await _context.Characters.
                Where(x => x.UserId == userId).ToListAsync();

            if (characters is null || characters.Count < 1)
            {
                return BadRequest("There are no characters");
            }

            return characters;
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> Post(CreateCharacterDto request)
        {
            var user = await _context.Users.FindAsync(request.UserId);

            if (user is null)
            {
                return BadRequest("User not found");
            }

            var newCharacter = new Character 
            { 
                Name = request.Name,
                RpgClass = request.RpgClass,
                User = user
            };


            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return await Get(newCharacter.UserId);
        }
    }
}
