
using System.IO;

namespace HousingRecommendationSystem
{
    public class FileManager : IFileManager
    {
        private string _filePath = "https://storage.googleapis.com/walkietechkie-bucket/ClipsScript.clp";

        public string GetClipsFilePath()
        {
            var tempPath = Path.GetTempPath() + "\\ClipsScript.clp";
            using (var wc = new System.Net.WebClient())
            {
                wc.DownloadFile(_filePath, tempPath);
            }

            return tempPath;
        }
    }
}