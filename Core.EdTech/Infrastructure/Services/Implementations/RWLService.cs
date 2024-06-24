using Core.EdTech.Domain.ViewModels;
using Core.EdTech.Infrastructure.Helpers.AppSettings;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.EdTech.Infrastructure.Services.Implementations
{
  public class RWLService : IRWLService
  {
    private readonly ConfigOptions configOptions;

    public RWLService(ConfigOptions configOptions)
    {
      this.configOptions = configOptions;
    }

    public async Task<string> CheckPoints(string RwlMemberId, string RwlParticipantId)
    { 
      var body = JsonConvert.SerializeObject(new RwlCheckPointRequest {
        RwlMemberId = RwlMemberId,
        RwlParticipantId = RwlParticipantId
      });
      
      return "6000";
    }

    public async Task<ServiceProviderResponse> GetServiceProviders(int RwlUserId)
    {
      var client = new RestClient(configOptions.RWLParams.BaseUrl);
      var requestToken = new Guid();
      Dictionary<string, string> headers = new();
      headers.Add("api-key", configOptions.RWLParams.ApiKey);
      headers.Add("hash", GenerateMD5($"{configOptions.RWLParams.ApiKey}{configOptions.RWLParams.ApiSecret}{requestToken.ToString()}"));
      headers.Add("request-token", requestToken.ToString());

      client.AddDefaultHeaders(headers);

      var request = new RestRequest($"/list-member-service-providers/{RwlUserId}");
      request.AddHeader("content-type", "application/json");

      var response = await client.GetAsync(request);

      Console.WriteLine();
      Console.WriteLine(response.Content);
      Console.WriteLine();

      return System.Text.Json.JsonSerializer.Deserialize<ServiceProviderResponse>(response.Content);
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

    public async Task<string> RedeemPoints(string RwlMemerId, string ProgramPrice, string RwlParticipantId, string discount, string multiple)
    {
      throw new NotImplementedException();
    }

    private string GenerateMD5(string yourString)
    {
      return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(yourString)).Select(s => s.ToString("x2")));
    }
  }
}
