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
      var user = userProfile.Repository.Get(x => x.RwlMemberId == request.Rwl_Member_Id).First();
      await voucherUoW.Repository.Insert(new Voucher
      {
        RequestId = request.Request_Id,
        Code = request.Voucher_Code,
        UserProfileId = user.Id
      });
      await voucherUoW.SaveAsync();

      return Ok();
    }

    public class RequestRequest
    {
      public string Request_Id { get; set; }
      public string Voucher_Code { get; set; }
      public int Rwl_Member_Id { get; set; }
    }
  }
}
