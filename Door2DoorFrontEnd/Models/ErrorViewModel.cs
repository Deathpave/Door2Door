namespace Door2DoorFrontEnd.Models
{
    // Model to handle error data
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}