namespace Core.EdTech.Infrastructure.Services.Abstracts
{
  public interface IAuthorizationManager
  {
    public void Register();
    public void SignIn();
    public void SignOut();
  }
}
