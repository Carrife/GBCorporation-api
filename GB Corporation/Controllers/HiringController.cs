using GB_Corporation.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GB_Corporation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HiringController : ControllerBase
    {
        private readonly IHiringService _hiringService;
        public HiringController(IHiringService hiringService)
        {
            _hiringService = hiringService;
        }
    }
}
