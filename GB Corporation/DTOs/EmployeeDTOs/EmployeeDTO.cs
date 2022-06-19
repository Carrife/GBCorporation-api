namespace GB_Corporation.DTOs.EmployeeDTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string SurnameRu { get; set; }
        public string PatronymicRu { get; set; }
        public string NameEn { get; set; }
        public string SurnameEn { get; set; }
        public string? WorkPhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ShortDTO Language { get; set; }
        public ShortDTO Department { get; set; }
        public ShortDTO Role { get; set; }
    }
}
