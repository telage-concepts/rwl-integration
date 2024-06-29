using Core.FinTech.Domain.DataAccess.Interfaces;
using Core.FinTech.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static FintechSim.Controllers.ConfirmRequestController;

namespace FintechSim.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AcceptVoucherController : ControllerBase
  {

    private readonly IUnitOfWork<Voucher> voucherUoW;
    private readonly IUnitOfWork<UserProfile> userProfile;

    [HttpPost]
    public async Task<IActionResult> Index(RequestRequest request)
    {
      var user = userProfile.Repository.Get(x => x.RwlMemberId == int.Parse(request.Rwl_Member_Id)).FirstOrDefault();
      await voucherUoW.Repository.Insert(new Voucher
      {
        Code = request.Voucher_Code,
        UserProfileId = user.Id,
        ProgramCode = request.Program_code,
        ProgramName = request.Program_name
      });
      await voucherUoW.SaveAsync();

      return Ok();
    }

    public class RequestRequest
    {
      public string Program_name { get; set; }
      public string Program_code { get; set; }
      public string Voucher_Code { get; set; }
      public string Rwl_Member_Id { get; set; }
    }
  }
}
