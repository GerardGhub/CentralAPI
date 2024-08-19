using CentralAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CentralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private static List<SampleModel> _samples = new List<SampleModel>();
        private readonly IHubContext<SampleHub> _hubContext;

        public SampleController(IHubContext<SampleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SampleModel>> GetSamples()
        {
            return Ok(_samples);
        }

        [HttpPost]
        public async Task<ActionResult<SampleModel>> AddSample(SampleModel model)
        {
            model.Id = _samples.Count + 1;
            model.CreatedAt = DateTime.Now;
            _samples.Add(model);

            //Notify the clients for the new data
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", model);

            return CreatedAtAction(nameof(GetSamples), new { id = model.Id }, model);
        }


    }
}
