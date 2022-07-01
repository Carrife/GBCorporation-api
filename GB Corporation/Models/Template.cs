namespace GB_Corporation.Models
{
    public class Template : BaseEntity
    {
        public string Name { get; set; }
        public string? Link { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
