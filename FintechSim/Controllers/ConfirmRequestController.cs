using Core.FinTech.Domain.DataAccess.Interfaces;
using Core.FinTech.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FintechSim.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ConfirmRequestController : Controller
  {
    private readonly IUnitOfWork<Request> unitOfWork;
    // GET: ConfirmRequestController
    [HttpPost]
    public ActionResult Index(RequestRequest request)
    {
      if (unitOfWork.Repository.Get(x => x.Id == Guid.Parse(request.Request_id)).FirstOrDefault() == null)
      {
        return Ok(new { Status = false });
      }
      return Ok(new {Status = true});
    }

    public class RequestRequest
    {
      public string Request_id { get; set; }
    }
  }
}
