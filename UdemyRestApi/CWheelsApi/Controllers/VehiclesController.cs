using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWheelsApi.Models;

namespace CWheelsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private static List<Vehicle> vehicles = new List<Vehicle>
        {
            new Vehicle(){ Id = 0, Title = "Tesla S", Price = 23000 },
            new Vehicle(){ Id = 1, Title = "Tesla X", Price = 29000 }
        };

        [HttpGet]
        public IEnumerable<Vehicle> Get()
        {
            return vehicles;
        }

        [HttpPost]
        public void Post([FromBody]Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }
    }
}
