using Core.FinTech.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FinTech.Infrastructure.Services.Abstracts
{
  public interface IRWLService
  {
    public Task<RWLInitResponse> Initialize(RWLUserDetail UserDetails);
    public Task<CourseDetails> GetCourseDetails(string ProgramCode);
    public Task<bool> RequestVoucher(RequestVoucherParams requestVoucherParams);
    public Task<bool> ConfirmVoucherRequest(string RequestId);
    public Task<(decimal DiscountPercentage, int Multiple)> ComputeDiscount(string RwlID, int RwlMemberId);
    public Task<bool> AcceptVoucher();
    public Task RedeemPoints(RedeemPointsParams redeemPointsParams);
  }
}
