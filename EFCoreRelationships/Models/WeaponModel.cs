using System.Text.Json.Serialization;

namespace EFCoreRelationships.Models
{
    public class WeaponModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; } = 10;
        [JsonIgnore]
        public CharacterModel Character { get; set; }
        public int CharacterId { get; set; }
    }
}
