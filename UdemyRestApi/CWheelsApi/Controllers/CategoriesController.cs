using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWheelsApi.Data;
using Microsoft.AspNetCore.Authorization;

namespace CWheelsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private CWheelsDbContext _cWheelsDbContext;
        public CategoriesController(CWheelsDbContext cWheelsDbContext)
        {
            _cWheelsDbContext = cWheelsDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _cWheelsDbContext.Categories;
            return Ok(categories);
        }
    }
}
