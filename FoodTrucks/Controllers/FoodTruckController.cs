using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GeoJSON.Net.Feature;
using System.Reflection;
using FoodTrucks.Services;

namespace FoodTrucks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodTruckController : ControllerBase
    {
        readonly ILogger<FoodTruckController> _logger;
        readonly IFoodTruckDataService _foodTruckDataService;

        public FoodTruckController(ILogger<FoodTruckController> logger,
                                   IFoodTruckDataService foodTruckDataService)
        {
            _logger               = logger;
            _foodTruckDataService = foodTruckDataService;
        }

        [HttpGet]
        public async Task<FeatureCollection> Get([FromQuery]FoodTruckFilter filter)
        {
            return await _foodTruckDataService.GetFoodTruckFeatures(filter);
        }
    }
}
