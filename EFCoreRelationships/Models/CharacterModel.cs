using System.Text.Json.Serialization;

namespace EFCoreRelationships.Models
{
    public class CharacterModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RpgClass { get; set; } = "Knight";
        [JsonIgnore]
        public UserModel User { get; set; }
        public int UserId { get; set; }
        public WeaponModel Weapon { get; set; }
        public List<SkillModel> Skills { get; set; }

    }
}
