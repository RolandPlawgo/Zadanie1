namespace Zadanie1_UI.Models
{
    internal class ErrorMessage
    {
        string type { get; set; }
        string title { get; set; }
        int status { get; set; }
        string traceId { get; set; }
        public Errors errors { get; set; }
    }
}
