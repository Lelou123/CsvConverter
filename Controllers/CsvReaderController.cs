using CsvConverter.Model.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CsvConverter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvReaderController : ControllerBase
    {
        private readonly ICsvService _csvService;

        public CsvReaderController(ICsvService csvService)
        {
            _csvService = csvService;
        }


        [HttpPost("CreateCsvObject")]
        public async Task<IActionResult> CreateCsvBankAsync(IFormFile file)
        {
            var result = await _csvService.CreateCsvObject(file);

            if (result.IsFailed) 
                return BadRequest(result.Errors.FirstOrDefault());

            return Ok(result.Value);
        }
    }
}
