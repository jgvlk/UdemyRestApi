using CWheelsApi.Data;
using CWheelsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        [HttpPost]
        [Authorize]
        public IActionResult Post(Vehicle vehicle)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = _cWheelsDbContext.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }

            var vehicleObj = new Vehicle()
            {
                Title = vehicle.Title,
                Description = vehicle.Description,
                Color = vehicle.Color,
                Company = vehicle.Company,
                Condition = vehicle.Condition,
                DatePosted = vehicle.DatePosted,
                Engine = vehicle.Engine,
                Price = vehicle.Price,
                Model = vehicle.Model,
                Location = vehicle.Location,
                CategoryId = vehicle.CategoryId,
                IsFeatured = false,
                IsHotAndNew = false,
                UserId = user.Id,
            };
            _cWheelsDbContext.Vehicles.Add(vehicleObj);
            _cWheelsDbContext.SaveChanges();

            return Ok(new { vehicleId = vehicleObj.Id, message = "Vechile added successfully" });
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult HotAndNewAds()
        {
            var vehicles = from v in _cWheelsDbContext.Vehicles
                           where v.IsHotAndNew == true
                           select new
                           {
                               VehicleId = v.Id,
                               VechileTitle = v.Title,
                               VehicleImageUrl = v.Images.FirstOrDefault().ImageUrl
                           };
            return Ok(vehicles);
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult SearchVehicles(string search)
        {
            var vehicles = from v in _cWheelsDbContext.Vehicles
                           where v.Title.StartsWith(search)
                           select new
                           {
                               VehicleId = v.Id,
                               VechileTitle = v.Title
                           };
            return Ok(vehicles);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetVehicles(int categoryId)
        {
            var vehicles = from v in _cWheelsDbContext.Vehicles
                           where v.CategoryId == categoryId
                           select new
                           {
                               VehicleId = v.Id,
                               VechileTitle = v.Title,
                               VehiclePrice = v.Price,
                               VehicleLocation = v.Location,
                               DatePosted = v.DatePosted,
                               IsFeatured = v.IsFeatured,
                               ImageUrl = v.Images.FirstOrDefault().ImageUrl

                           };
            return Ok(vehicles);
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult VehicleDetails(int Id)
        {
            var foundVehicle = _cWheelsDbContext.Vehicles.Find(Id);
            if (foundVehicle == null)
            {
                return NoContent();
            }
            var vehicle = from v in _cWheelsDbContext.Vehicles
                           join u in _cWheelsDbContext.Users on v.UserId equals u.Id
                           where v.Id == Id
                           select new
                           {
                               VehicleId = v.Id,
                               VechileTitle = v.Title,
                               VehicleDescription = v.Description,
                               VechilePrice = v.Price,
                               VehicleModel = v.Model,
                               VehicleEngine = v.Engine,
                               VehicleColor = v.Color,
                               VehicleCompany = v.Company,
                               DatePosted = v.DatePosted,
                               VehicleCondition = v.Condition,
                               VehicleLocation = v.Location,
                               Images = v.Images,
                               Email = u.Email,
                               Phone = u.Phone,
                               ImageUrl = u.ImageUrl
                           };
            return Ok(vehicle);
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult MyAds()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = _cWheelsDbContext.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }
            var vehicles = from v in _cWheelsDbContext.Vehicles
                          where v.UserId == user.Id
                          select new
                          {
                              VehicleId = v.Id,
                              VechileTitle = v.Title,
                              VechilePrice = v.Price,
                              VehicleLocation = v.Location,
                              Images = v.Images,
                              DatePosted = v.DatePosted,
                              IsFeatured = v.IsFeatured,
                              ImageUrl = v.Images.FirstOrDefault().ImageUrl
                          };
            return Ok(vehicles);
        }
    }
}
