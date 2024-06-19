using Core.FinTech.Domain.DataAccess.Interfaces;
using Core.FinTech.Domain.Entities;
using Core.FinTech.Domain.ViewModels;
using Core.FinTech.Infrastructure.Helpers.AppSettings;
using Core.FinTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FintechSim.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InitiateVoucherRequestController : ControllerBase
  {
    private readonly ConfigOptions configOptions;
    private readonly IRWLService rWLService;
    private readonly IUnitOfWork<Request> unitOfWork;
    

    // GET: api/<InitiateVoucherRequestController>
    public async Task<IActionResult> Index(string RwlProgramCode, string Reference, string RwlMemberId)
    {
      var client = new RestClient("https://api.paystack.co/");

      client.AddDefaultHeader("Authorization", $"Bearer {configOptions.Paystack.SecretKey}");

      var request = new RestRequest($"transaction/verify/{Reference.ToString()}");

      var response = client.Get(request);
      if (!response.IsSuccessful || response.Content == null)
      {
        //Log Error
        return Ok(new { success = false });
      }

      var responseObj = JsonConvert.DeserializeObject<PaystackVerification>(response.Content);

      if (responseObj.data.status == "success")
      {
        var req = new Request { };
        await unitOfWork.Repository.Insert(req);
        await unitOfWork.SaveAsync();

        rWLService.RequestVoucher(new RequestVoucherParams {
          RequestId = req.Id.ToString(),
          RWLMemberId = RwlMemberId,
          CourseId = RwlProgramCode
        });
        return Ok(new { success = true });
      }
      return Ok(new { success = false });
    }
  }
}
