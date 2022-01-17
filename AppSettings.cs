namespace CinemateAPI
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public int Expiration { get; set; }
    }

    public class MovieDbConfig
    {
        public string ClientName { get; set; }

        public string BaseUrl { get; set; }

        public string ApiKey { get; set; }

        public string BasePictureUrl { get; set; }
    }
}
