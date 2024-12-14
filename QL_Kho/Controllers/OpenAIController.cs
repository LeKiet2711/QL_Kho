using Microsoft.AspNetCore.Mvc;
using QL_Kho.Service;
using System.Threading.Tasks;

namespace QL_Kho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly OpenAI_Service _openAIService;

        public OpenAIController(OpenAI_Service openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PromptRequest request)
        {
            var response = await _openAIService.GetResponseFromOpenAI(request.Prompt);
            return Ok(new { response });
        }
    }

    public class PromptRequest
    {
        public string Prompt { get; set; } = string.Empty;
    }
}
