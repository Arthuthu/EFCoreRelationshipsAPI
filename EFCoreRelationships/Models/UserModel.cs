﻿namespace EFCoreRelationships.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<CharacterModel> Characters { get; set; }
    }
}
