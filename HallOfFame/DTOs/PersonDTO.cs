namespace HallOfFame.DTOs
{
    public class PersonDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<SkillDTO> Skills { get; set; }
    }
}