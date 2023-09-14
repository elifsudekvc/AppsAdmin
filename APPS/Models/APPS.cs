namespace AppsServices.Models
{
    public class APPS
    {
        public int AppID { get; set; }
        public string AppName { get; set; } = string.Empty;
        public int Ratings { get; set; }
        public string info { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;

        public string Response { get; set; } = string.Empty;

    }
}
