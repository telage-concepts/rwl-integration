using Core.EdTech.Domain.ViewModels;

namespace Core.EdTech.Infrastructure.Services.Abstracts
{
  public interface IRWLService
  {
    public Task<RWLInitResponse> Initialize(RWLUserDetail UserDetails);
    public Task<ServiceProviderResponse> GetServiceProviders(int RwlUserId);
    public Task<string> CheckPoints(string RwlMemberId, string RwlParticipantId);
    public Task<string> RedeemPoints(string RwlMemerId, string ProgramPrice, string RwlParticipantId, string discount, string multiple);
  }
}
