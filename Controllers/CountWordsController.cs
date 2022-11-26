using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HemtestRESTAPI.Services;

namespace HemtestRESTAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class CountWordsController : ControllerBase
        {
        
        private readonly ILogger<CountWordsController> _logger;

        public CountWordsController(ILogger<CountWordsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{text}")]
        public ActionResult<string> Get(string text){

            /*
             * For further development,updates and maintenance we might want to use headers to check for example different client-versions
             

            if (Request.Headers.TryGetValue("Version", out var clientversion)){

                //If needed handle different client versions here
                return "correct version";

            } else {

            }
            */
            return CountWordsService.CountWords(text);

        }




    }
}
