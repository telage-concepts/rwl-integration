using Core.EdTech.Domain.DataAccess.Interfaces;
using Core.EdTech.Domain.Entities;
using Core.EdTech.Domain.Entities.Identity;
using Core.EdTech.Domain.ViewModels;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EdtechSim.Pages
{
  [Authorize]
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly IRWLService _rdfService;
    private readonly IUnitOfWork<UserProfile> unitOfWork;
    private readonly IUnitOfWork<DiscountWallet> dwUoW;
    private readonly UserManager<ApplicationUser> userManager;

    public UserProfile UserProfile { get; set; }
    public List<ServiceProviderResponse> ServiceProviderResponses { get; set; }
    public List<SelectListItem> ServiceProviderSelectListItems { get; set; }
    public DiscountWallet wallet { get; set; }
    public IndexModel(ILogger<IndexModel> logger, IRWLService rdfService, IUnitOfWork<UserProfile> unitOfWork, IUnitOfWork<DiscountWallet> dwUoW, UserManager<ApplicationUser> userManager)
    {
      _logger = logger;
      _rdfService = rdfService;
      this.unitOfWork = unitOfWork;
      this.dwUoW = dwUoW;
      this.userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
      var user = await userManager.GetUserAsync(User);
      UserProfile = unitOfWork.Repository.Get(x => x.Email == user.Email).FirstOrDefault();
      wallet = dwUoW.Repository.Get(x => x.UserProfileId == UserProfile.Id).FirstOrDefault();
      if(wallet == null)
      {
        await dwUoW.Repository.Insert(new DiscountWallet
        {
          UserProfileId = UserProfile.Id,
          Currency = WalletCurrency.Naira,
          Balance = 0
        });

        await dwUoW.SaveAsync();
        wallet = dwUoW.Repository.Get(x => x.UserProfileId == UserProfile.Id).First();
      }

      if (UserProfile.RwlMemberId == null)
      {
        UserProfile.RwlMemberId = (await _rdfService.Initialize(new RWLUserDetail
        {
          Email = UserProfile.Email,
          FirstName = UserProfile.FirstName,
          LastName = UserProfile.LastName,
          PhoneNumber = UserProfile.PhoneNumber
        })).RwlMemberId;
      }

      ServiceProviderResponses = await _rdfService.GetServiceProviders(UserProfile.RwlMemberId);

      ServiceProviderSelectListItems = new();

      foreach(var provider in ServiceProviderResponses)
      {
        ServiceProviderSelectListItems.Add(new SelectListItem { Text = provider.ServiceProviderName, Value = provider.ServiceProviderId });
      }
      return Page();
    }
  }
}
