namespace GB_Corporation.DTOs
{
    public class EmailDTO
    {
        public string Subject { get; set; }

        public string Text { get; set; }

        public List<int> SendTo { get; set; }
    }
}
