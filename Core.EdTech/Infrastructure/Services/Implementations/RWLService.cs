using Core.EdTech.Domain.ViewModels;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EdTech.Infrastructure.Services.Implementations
{
  public class RWLService : IRWLService
  {
    public async Task<string> CheckPoints(string RwlMemberId, string RwlParticipantId)
    { 
      var body = JsonConvert.SerializeObject(new RwlCheckPointRequest {
        RwlMemberId = RwlMemberId,
        RwlParticipantId = RwlParticipantId
      });

      return "6000";
    }

    public async Task<List<ServiceProviderResponse>> GetServiceProviders(string RwlUserId)
    {
      return new List<ServiceProviderResponse> { new ServiceProviderResponse
      {
        ServiceProviderId = "1",
        ServiceProviderName = "Blip School"
      },
      new ServiceProviderResponse
      {
        ServiceProviderId = "2",
        ServiceProviderName = "Alt School"
      }
      }; 
    }

    public async Task<RWLInitResponse> Initialize(RWLUserDetail UserDetails)
    {
      var requestData = JsonConvert.SerializeObject(UserDetails);
      return new RWLInitResponse
      {
        RwlMemberId = "1075EIN5588"
      };
    }

    public async Task<string> RedeemPoints(string RwlMemerId, string ProgramPrice, string RwlParticipantId, string discount, string multiple)
    {
      throw new NotImplementedException();
    }
  }
}
