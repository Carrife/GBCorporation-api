namespace GB_Corporation.DTOs
{
    public class HiringFilterDTO
    {
        public HiringFilterDTO(string? nameEn, string? surnameEn, string? login, int?[] positionIds, int?[] statusIds)
        {
            NameEn = nameEn;
            SurnameEn = surnameEn;
            Login = login;
            PositionIds = positionIds;
            StatusIds = statusIds;
        }

        public string? NameEn { get; }
        public string? SurnameEn { get; }
        public string? Login { get; }
        public int?[] PositionIds { get; }
        public int?[] StatusIds { get; }
    }
}
