global using EFCoreRelationships.Data;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
using EFCoreRelationships.Dtos;
using EFCoreRelationships.Models;

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
        public async Task<ActionResult<List<CharacterModel>>> Get(int userId)
        {

            var characters = await _context.Characters.
                Where(x => x.UserId == userId)
                .Include(x => x.Weapon)
                .Include(x => x.Skills)
                .ToListAsync();

            if (characters is null || characters.Count < 1)
            {
                return BadRequest("There are no characters");
            }

            return characters;
        }

        [HttpPost]
        public async Task<ActionResult<List<CharacterModel>>> PostCharacter(CreateCharacterDto request)
        {
            var user = await _context.Users.FindAsync(request.UserId);

            if (user is null)
            {
                return BadRequest("User not found");
            }

            var newCharacter = new CharacterModel 
            { 
                Name = request.Name,
                RpgClass = request.RpgClass,
                User = user
            };


            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return await Get(newCharacter.UserId);
        }

        [HttpPost("weapon")]
        public async Task<ActionResult<CharacterModel>> PostWeapon(AddWeaponDto request)
        {
            var character = await _context.Characters.FindAsync(request.CharacterId);

            if (character is null)
            {
                return BadRequest("Character not found");
            }

            var newWeapon = new WeaponModel
            {
                Name = request.Name,
                Damage = request.Damage,
                Character = character
            };


            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();

            return character;
        }

        [HttpPost("skill")]
        public async Task<ActionResult<CharacterModel>> PostSkill(CharacterSkillDto request)
        {
            var character = await _context.Characters.Where(x => x.Id == request.CharacterId)
                .Include(x => x.Skills)
                .FirstOrDefaultAsync();

            if (character is null)
            {
                return BadRequest("Character not found");
            }

            var skill = await _context.Skills.FindAsync(request.SkillId);

            if (skill is null)
            {
                return BadRequest("Skill not found");
            }


            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return character;
        }
    }
}
