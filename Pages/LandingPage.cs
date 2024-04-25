using Microsoft.Playwright;

namespace RecruitmentUiTest.Pages;

public class LandingPage(IPage page)
{
   private readonly IPage _page = page;

    public ILocator EmailInputLocator => _page.Locator("#email");
    public ILocator PasswordInputLocator => _page.Locator("#password");
    public ILocator SubmitButtonLocator => _page.Locator("#next");
}