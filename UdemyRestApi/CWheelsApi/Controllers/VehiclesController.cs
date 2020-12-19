using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWheelsApi.Data;
using CWheelsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CWheelsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {


        private CWheelsDbContext _cWheelsDbContext;

        public VehiclesController(CWheelsDbContext cWheelsDbContext)
        {
            _cWheelsDbContext = cWheelsDbContext;
        }


        // GET: api/<VehiclesController>
        [HttpGet]
        public IEnumerable<Vehicle> Get()
        {
            return _cWheelsDbContext.Vehicles;
        }

        // GET api/<VehiclesController>/5
        [HttpGet("{id}")]
        public Vehicle Get(int id)
        {
            var vehicle = _cWheelsDbContext.Vehicles.Find(id);
            return vehicle;
        }

        // POST api/<VehiclesController>
        [HttpPost]
        public void Post([FromBody] Vehicle vehicle)
        {
            _cWheelsDbContext.Vehicles.Add(vehicle);
            _cWheelsDbContext.SaveChanges();
        }

        // PUT api/<VehiclesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Vehicle vehicle)
        {
            var entity = _cWheelsDbContext.Vehicles.Find(id);
            entity.Title = vehicle.Title;
            entity.Price = vehicle.Price;
            _cWheelsDbContext.SaveChanges();
        }

        // DELETE api/<VehiclesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var vehicle = _cWheelsDbContext.Vehicles.Find(id);
            _cWheelsDbContext.Vehicles.Remove(vehicle);
            _cWheelsDbContext.SaveChanges();
        }
    }
}
