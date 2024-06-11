using Core.FinTech.Domain.ViewModels;
using Core.FinTech.Infrastructure.Services.Abstracts;
using Newtonsoft.Json;

namespace Core.FinTech.Infrastructure.Services.Implementations
{
  public class RWLService : IRWLService
  {
    public Task<bool> AcceptVoucher()
    {
      throw new NotImplementedException();
    }

    public Task<(float Partialdiscount, int numberOfFullDiscount)> ComputeDiscount(string RwlID)
    {
      throw new NotImplementedException();
    }

    public Task<bool> ConfirmVoucherRequest(string RequestId)
    {
      throw new NotImplementedException();
    }

    public async Task<CourseDetails> GetCourseDetails(string ProgramCode)
    {
      var data = new CourseDetails
      {
        ProgramName = "creating tech social club",
        ProgramCode = "BLP-9269YXD9901",
        ProgramImage = "https://res.cloudinary.com/kenrealtor/image/upload/v1712063277/cjisievpo2u0u7nakkil.jpg",
        ProgramFee = 150000
      };
      return data;
    }

    public async Task<RWLInitResponse> Initialize(RWLUserDetail UserDetails)
    {
      var requestData = JsonConvert.SerializeObject(UserDetails);
      return new RWLInitResponse
      {
        RwlMemberId = "1075EIN5588"
      };
    }

    public Task RedeemPoints(RedeemPointsParams redeemPointsParams)
    {
      throw new NotImplementedException();
    }

    public Task<bool> RequestVoucher(RequestVoucherParams requestVoucherParams)
    {
      throw new NotImplementedException();
    }
  }
}
