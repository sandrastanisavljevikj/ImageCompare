using ImageCompareApi.Models;

namespace ImageCompareApi.Services
{
    public interface ICompareService
    {
       double Compare(string imagePath, ApiConfig configuration, bool front = true);
    }
}