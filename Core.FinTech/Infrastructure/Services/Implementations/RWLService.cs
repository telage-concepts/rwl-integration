using Core.FinTech.Domain.DataAccess.Interfaces;
using Core.FinTech.Domain.Entities;
using Core.FinTech.Domain.ViewModels;
using Core.FinTech.Infrastructure.Helpers.AppSettings;
using Core.FinTech.Infrastructure.Services.Abstracts;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace Core.FinTech.Infrastructure.Services.Implementations
{
  public class RWLService : IRWLService
  {
    private readonly ConfigOptions configOptions;
    private readonly IUnitOfWork<DiscountWallet> dwUoW;
    private readonly IUnitOfWork<UserProfile> userProfileUoW;

    public RWLService(ConfigOptions configOptions, IUnitOfWork<UserProfile> userProfileUoW, IUnitOfWork<DiscountWallet> dwUoW)
    {
      this.configOptions = configOptions;
      this.userProfileUoW = userProfileUoW;
      this.dwUoW = dwUoW;
    }

    public Task<bool> AcceptVoucher()
    {
      throw new NotImplementedException();
    }

    public async Task<(decimal DiscountPercentage, int Multiple)> ComputeDiscount(string RwlID, int RwlMemberId)
    {
      var user = userProfileUoW.Repository.Get(x => x.RwlMemberId == RwlMemberId).FirstOrDefault();
      var wallet = dwUoW.Repository.Get(x => x.UserProfileId == user.Id).First();

      var course = await GetCourseDetails(RwlID);

      var courseCost = decimal.Parse(course.Data.ProgramFee);

      var discount = (wallet.Balance / courseCost) * 100;
      if(discount < 100)
      {
        return (discount, 0);
      }
      return (100, decimal.ToInt32(discount/100));
    }

    public Task<bool> ConfirmVoucherRequest(string RequestId)
    {
      throw new NotImplementedException();
    }

    public async Task<CourseDetails> GetCourseDetails(string ProgramCode)
    {
      var client = new RestClient(configOptions.RWLParams.BaseUrl);
      var requestToken = new Guid();
      Dictionary<string, string> headers = new();
      headers.Add("api-key", configOptions.RWLParams.ApiKey);
      headers.Add("hash", GenerateMD5($"{configOptions.RWLParams.ApiKey}{configOptions.RWLParams.ApiSecret}{requestToken.ToString()}"));
      headers.Add("request-token", requestToken.ToString());
      client.AddDefaultHeaders(headers);

      var request = new RestRequest($"/request-program-details/{ProgramCode}");

      var response = await client.GetAsync(request);

      if (!response.IsSuccessful || response.Content == null)
      {
        //Log Error
        return default;
      }

      return System.Text.Json.JsonSerializer.Deserialize<CourseDetails>(response.Content);
    }

    public async Task<RWLInitResponse> Initialize(RWLUserDetail UserDetails)
    {

      var client = new RestClient(configOptions.RWLParams.BaseUrl);
      var requestToken = new Guid();
      Dictionary<string, string> headers = new();
      headers.Add("api-key", configOptions.RWLParams.ApiKey);
      headers.Add("hash", GenerateMD5($"{configOptions.RWLParams.ApiKey}{configOptions.RWLParams.ApiSecret}{requestToken.ToString()}"));
      headers.Add("request-token", requestToken.ToString());

      client.AddDefaultHeaders(headers);

      var request = new RestRequest("/init");
      request.AddHeader("content-type", "application/json");
      request.AddBody(System.Text.Json.JsonSerializer.Serialize(UserDetails));

      var response = await client.PostAsync(request);


      Console.WriteLine("");
      Console.WriteLine($"Content : {response.Content}");
      Console.WriteLine("");

      return System.Text.Json.JsonSerializer.Deserialize<RWLInitResponse>(response.Content);
    }

    public Task RedeemPoints(RedeemPointsParams redeemPointsParams)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> RequestVoucher(RequestVoucherParams requestVoucherParams)
    {

      var client = new RestClient(configOptions.RWLParams.BaseUrl);
      var requestToken = new Guid();
      Dictionary<string, string> headers = new();
      headers.Add("api-key", configOptions.RWLParams.ApiKey);
      headers.Add("hash", GenerateMD5($"{configOptions.RWLParams.ApiKey}{configOptions.RWLParams.ApiSecret}{requestToken.ToString()}"));
      headers.Add("request-token", requestToken.ToString());
      client.AddDefaultHeaders(headers);

      var request = new RestRequest("/request-voucher");
      request.AddHeader("content-type", "application/json");
      request.AddBody(System.Text.Json.JsonSerializer.Serialize(requestVoucherParams));

      var response = await client.PostAsync(request);

      if (!response.IsSuccessful)
      {
        //Log Error
        return false;
      }

      return true;
    }

    private string GenerateMD5(string yourString)
    {
      return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(yourString)).Select(s => s.ToString("x2")));
    }
  }
}
