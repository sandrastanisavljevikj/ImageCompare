using System;
using System.IO;
using ImageCompareApi.Models;
using ImageMagick;

namespace ImageCompareApi.Services
{
    public class CompareService : ICompareService
    {
        const int Size = 150;
        const int Quality = 75;
        private string _resizedImagePath;
        private string _resizedCompareImagePath;

        public double Compare(string imagePath, ApiConfig configuration, bool front = true)
        {
            if (!File.Exists($"{Directory.GetCurrentDirectory()}{@"\wwwroot\"}{imagePath}"))
            {
                throw new Exception("The requested file is not uploaded.");
            }
            _resizedImagePath = ResizeImage(imagePath);
            _resizedCompareImagePath = front ? ResizeImage(configuration.FrontReferenceImagePath) : ResizeImage(configuration.BackReferenceImagePath);

            using (var resizedImage = new MagickImage(_resizedImagePath))
            {
                return resizedImage.Compare(new MagickImage(_resizedCompareImagePath), ErrorMetric.NormalizedCrossCorrelation);
            }
            
        }

        private string ResizeImage(string imagePath)
        {
            var combined = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\"}{imagePath}";
            var formated = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\formatted\"}{imagePath}";
            using (var image = new MagickImage(combined))
            {
                image.Resize(Size, Size);
                image.Strip();
                image.Quality = Quality;
                image.Grayscale();
                image.Write(formated);
            }
            return formated;
        }
    }
}