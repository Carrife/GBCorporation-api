using System.Text.Json.Serialization;

namespace GB_Corporation.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string SurnameRu { get; set; }
        public string PatronymicRu { get; set; }
        public string NameEn { get; set; }
        public string SurnameEn { get; set; }
        public string Login { get; set; }
        public string? WorkPhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public int? LanguageId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public SuperDictionary Department { get; set; }
        public SuperDictionary Language { get; set; }
        public SuperDictionary Status { get; set; }
        public Role Role { get; set; }
    }
}
