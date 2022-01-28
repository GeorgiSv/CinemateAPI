namespace CinemateAPI.Data
{
    public class DataValidations
    {
        public class Review
        {
            public const int MaxContentLength = 2000;
            public const int MinContentLength = 5;
            public const int MaxTitleLength = 150;
            public const int MinTitleLength = 3;
        }
    }
}
