using DotNetCore_Day2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidController : ControllerBase
    {
        private readonly IGuidTransientService _TS1;
        private readonly IGuidTransientService _TS2;

        private readonly IGuidSingletonService _SS1;
        private readonly IGuidSingletonService _SS2;

        private readonly IGuidScopedService _ScS1;
        private readonly IGuidScopedService _ScS2;
        public GuidController(IGuidTransientService ts1,
                              IGuidTransientService ts2,
                              IGuidSingletonService ss1,
                              IGuidSingletonService ss2,
                              IGuidScopedService scs1,
                              IGuidScopedService scs2
                              ) 
        {
            _TS1 = ts1;
            _TS2 = ts2;
            _SS1 = ss1;
            _SS2 = ss2;
            _ScS1 = scs1;
            _ScS2 = scs2;
        }

        [HttpGet]
        public IActionResult GetGuid() {
            var result =
            new {
                Trasinet1 = _TS1.Get(), Trasinet2 = _TS2.Get(),

                Singleton1 = _SS1.Get(), Singleton2 = _SS2.Get(),

                Scoped1 = _ScS1.Get(), Scoped2 = _ScS2.Get()

                
            };
            return Ok( result );
        }
    }
}
