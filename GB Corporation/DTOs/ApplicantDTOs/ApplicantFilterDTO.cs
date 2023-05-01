namespace GB_Corporation.DTOs
{
    public class ApplicantFilterDTO
    {
        public ApplicantFilterDTO(string? nameRu, string? surnameRu, string? patronymicRu, string? nameEn, string? surnameEn, 
            string? login, int?[] statusIds)
        {
            NameRu = nameRu;
            SurnameRu = surnameRu;
            PatronymicRu = patronymicRu;
            NameEn = nameEn;
            SurnameEn = surnameEn;
            Login = login;
            StatusIds = statusIds;
        }

        public string? NameRu { get; }
        public string? SurnameRu { get; }
        public string? PatronymicRu { get; }
        public string? NameEn { get; }
        public string? SurnameEn { get; }
        public string? Login { get; }
        public int?[] StatusIds { get; }
    }
}
