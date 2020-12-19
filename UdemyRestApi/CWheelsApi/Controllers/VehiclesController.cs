using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWheelsApi.Data;
using CWheelsApi.Models;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Get()
        {
            return Ok(_cWheelsDbContext.Vehicles);

        }

        // GET api/<VehiclesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var vehicle = _cWheelsDbContext.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicle);
            }
        }

        // POST api/<VehiclesController>
        [HttpPost]
        public IActionResult Post([FromBody] Vehicle vehicle)
        {
            _cWheelsDbContext.Vehicles.Add(vehicle);
            _cWheelsDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<VehiclesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Vehicle vehicle)
        {
            var entity = _cWheelsDbContext.Vehicles.Find(id);
            if (entity == null)
            {
                return NotFound("No record found with the provided Id");
            }
            else
            {
                entity.Title = vehicle.Title;
                entity.Price = vehicle.Price;
                _cWheelsDbContext.SaveChanges();
                return Ok("Record updated successfully");
            }
        }

        // DELETE api/<VehiclesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehicle = _cWheelsDbContext.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound("No record found with the provided Id");
            }
            else
            {
                _cWheelsDbContext.Vehicles.Remove(vehicle);
                _cWheelsDbContext.SaveChanges();
                return Ok("Record deleted");
            }
        }
    }
}
