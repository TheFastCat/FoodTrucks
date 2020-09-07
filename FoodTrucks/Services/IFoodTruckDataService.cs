using GeoJSON.Net.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrucks.Services
{
    public interface IFoodTruckDataService
    {
       Task<FeatureCollection> GetFoodTruckFeatures(FoodTruckFilter filter);
    }
}
