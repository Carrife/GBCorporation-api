namespace GB_Corporation.DTOs
{
    public class TestProgressFilterDTO
    {
        public TestProgressFilterDTO(string? login = null, string? test = null, int?[] statusIds = null)
        {
            Login = login;
            Test = test;
            StatusIds = statusIds;
        }

        public string? Login { get; }
        public string? Test { get; }
        public int?[] StatusIds { get; }
    }
}
