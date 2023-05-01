namespace GB_Corporation.DTOs
{
    public class UsersFilterDTO
    {
        public UsersFilterDTO(string? nameRu, string? surnameRu, string? patronymicRu, string? nameEn, string? surnameEn, string? login, 
            int?[] roleIds)
        {
            NameRu = nameRu;
            SurnameRu = surnameRu;
            PatronymicRu = patronymicRu;
            NameEn = nameEn;
            SurnameEn = surnameEn;
            Login = login;
            RoleIds = roleIds;
        }

        public string? NameRu { get; }
        public string? SurnameRu { get; }
        public string? PatronymicRu { get; }
        public string? NameEn { get; }
        public string? SurnameEn { get; }
        public string? Login { get; }
        public int?[] RoleIds { get; }
    }
}
