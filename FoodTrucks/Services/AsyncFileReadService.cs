using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace FoodTrucks.Services
{
    public class AsyncFileReadService : IAsyncFileReadService
    {
        public async Task<string> ReadAllText(string filePath)
        {
            var stringBuilder = new StringBuilder();
            using (var fileStream   = File.OpenRead(filePath))
            using (var streamReader = new StreamReader(fileStream))
            {
                string line = await streamReader.ReadLineAsync();
                while (line != null)
                {
                    stringBuilder.AppendLine(line);
                    line = await streamReader.ReadLineAsync();
                }
                return stringBuilder.ToString();
            }
        }
    }
}
