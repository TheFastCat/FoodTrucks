using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrucks.Services
{
    public interface IAsyncFileReadService
    {
        Task<string> ReadAllText(string filePath);
    }
}
