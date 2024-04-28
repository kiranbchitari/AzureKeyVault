using Microsoft.AspNetCore.Mvc;

namespace AzureKeyVault.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SecretController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string secretValue = _configuration["Kiran"];
            return Ok(secretValue);
        }
    }
}
