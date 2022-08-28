namespace GB_Corporation.DTOs
{
    public class ErrorResponseDTO
    {
        public int ErrorStatus { get; set; }

        public ErrorResponseDTO(int errorStatus)
        {
            ErrorStatus = errorStatus;
        }
    }
}
