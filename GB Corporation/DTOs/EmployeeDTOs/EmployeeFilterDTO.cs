namespace GB_Corporation.DTOs
{
    public class EmployeeFilterDTO
    {
        public EmployeeFilterDTO(string? nameRu, string? surnameRu, string? patronymicRu, string? nameEn, string? surnameEn, 
            string? login, int?[] departmentIds, int?[] positionIds, int?[] statusIds)
        {
            NameRu = nameRu;
            SurnameRu = surnameRu;
            PatronymicRu = patronymicRu;
            NameEn = nameEn;
            SurnameEn = surnameEn;
            Login = login;
            DepartmentIds = departmentIds;
            PositionIds = positionIds;
            StatusIds = statusIds;
        }

        public string? NameRu { get; }
        public string? SurnameRu { get; }
        public string? PatronymicRu { get; }
        public string? NameEn { get; }
        public string? SurnameEn { get; }
        public string? Login { get; }
        public int?[] DepartmentIds { get; }
        public int?[] PositionIds { get; }
        public int?[] StatusIds { get; }
    }
}
