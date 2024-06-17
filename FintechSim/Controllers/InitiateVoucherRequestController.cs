using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FintechSim.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InitiateVoucherRequestController : ControllerBase
  {
    // GET: api/<InitiateVoucherRequestController>
    public ActionResult Index(string RwlProgramCode, string Reference)
    {
      return Ok(new { Status = true });
    }
  }
}
