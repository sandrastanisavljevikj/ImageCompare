namespace ImageCompareApi.Models
{
    public class ApiConfig
    {
        public string FrontReferenceImagePath { get; set; }
        public string BackReferenceImagePath { get; set; }
        public double NotOkayTreshold { get; set; }
        public double FailedTreshold { get; set; }
    }
}
