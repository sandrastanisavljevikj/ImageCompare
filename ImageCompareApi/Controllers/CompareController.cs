using System;
using System.Threading.Tasks;
using ImageCompareApi.Models;
using ImageCompareApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ImageCompareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompareController : Controller
    {
        private readonly ICompareService _service;
        private ApiConfig _configuration;
        public CompareController(ICompareService service, IOptions<ApiConfig> configuration)
        {
            _service = service;
            _configuration = configuration.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RequestBody input)
        {
            try
            {
                if (!string.IsNullOrEmpty(input.backImage))
                {
                    var frontTask = Task.Run(() => _service.Compare(input.frontImage, _configuration));
                    var backTask = Task.Run(() => _service.Compare(input.backImage, _configuration, false));
                    await Task.WhenAll(frontTask, backTask);

                    if (frontTask.Result + backTask.Result >= _configuration.FailedTreshold)
                        return Ok(ResultEnum.Fail);

                    if (frontTask.Result + backTask.Result >= _configuration.NotOkayTreshold)
                        return Ok(ResultEnum.NotOk);
                }
                else
                {
                    var frontTask = await Task.Run(() => _service.Compare(input.frontImage, _configuration));

                    if (frontTask >= _configuration.NotOkayTreshold)
                        return Ok(ResultEnum.NotOk);

                    if (frontTask >= _configuration.FailedTreshold)
                        return Ok(ResultEnum.Fail);
                }

                return Ok(ResultEnum.Ok);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
