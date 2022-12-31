using System.Text.Json.Serialization;

namespace EFCoreRelationships.Models
{
    public class SkillModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        [JsonIgnore]
        public List<CharacterModel> Characters { get; set; }
    }
}
