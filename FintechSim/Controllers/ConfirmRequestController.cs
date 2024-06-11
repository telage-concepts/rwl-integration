using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FintechSim.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ConfirmRequestController : Controller
  {
    // GET: ConfirmRequestController
    public ActionResult Index(string RequestId)
    {
      return Ok(new {Status = true});
    }

  }
}
