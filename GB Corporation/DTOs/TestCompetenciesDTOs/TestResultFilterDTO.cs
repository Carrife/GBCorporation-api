namespace GB_Corporation.DTOs
{
    public class TestResultFilterDTO
    {
        public TestResultFilterDTO(string? login = null, string? test = null)
        {
            Login = login;
            Test = test;
        }

        public string? Login { get; }
        public string? Test { get; }
    }
}
