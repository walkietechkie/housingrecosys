
using System.IO;

namespace HousingRecommendationSystem
{
    public class FileManager : IFileManager
    {
        private string _filePath = "https://storage.googleapis.com/walkietechkie-bucket/ClipsScriptV3.clp";

        public string GetClipsFilePath()
        {
            var tempPath = Path.GetTempPath() + "ClipsScriptV3.clp";

            using (var wc = new System.Net.WebClient())
            {
                wc.DownloadFile(_filePath, tempPath);
            }
            return tempPath;
        }
    }
}