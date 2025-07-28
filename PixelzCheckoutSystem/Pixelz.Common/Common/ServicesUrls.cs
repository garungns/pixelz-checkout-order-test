namespace Pixelz.Models.Common
{
    public class ServiceUrls
    {
        public ServiceEndpoint Payment { get; set; }
        public ServiceEndpoint Production { get; set; }
        public ServiceEndpoint Email { get; set; }
    }

    public class ServiceEndpoint
    {
        public string BaseUrl { get; set; }
        public Dictionary<string, string> Endpoints { get; set; }
    }
}
