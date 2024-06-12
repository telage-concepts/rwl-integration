using Core.EdTech.Domain.DataAccess.Interfaces;
using Core.EdTech.Domain.Entities;
using Core.EdTech.Domain.Entities.Identity;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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


      return Page();
    }
  }
}
