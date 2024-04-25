using Microsoft.Playwright;

namespace RecruitmentUiTest.Pages;

public class SignUpPage(IPage page)
{
    private readonly IPage _page = page;

    public ILocator AddVatNumberLocator(string country) => _page.Locator("app-add-country-vatnumber div").Filter(new() { HasText = country + " Help me get a VAT number" }).GetByRole(AriaRole.Button);
    public ILocator GetByLabelLocator(string label) => _page.GetByLabel(label);
    public ILocator CompanyLegalNameOfBusiness => _page.Locator("#companyLegalNameOfBusiness");
    public ILocator GetByPlaceholderLocator(string placeholder) => _page.GetByPlaceholder(placeholder);
    public ILocator OptionRoleLocator(string option) => _page.GetByRole(AriaRole.Option, new() { Name = option });
    public ILocator GetByExactTextLocator(string text) => _page.GetByText(text, new() { Exact = true } );
    public ILocator NextStepLocator => _page.GetByRole(AriaRole.Button, new() { Name = "Next step" });
    public ILocator OpenBusinessLocationDropDownLocator() => _page.GetByText("Where is your business located? ×");
    public ILocator SelectBusinessLocationSelectorLocator(string country) => _page.GetByRole(AriaRole.Option, new() { Name = country });
    public ILocator SelectServiceCountryLocator(string country) => _page.GetByRole(AriaRole.Button, new() { Name = country });
    public ILocator EditSubscriptionLocator => _page.Locator("app-step-header").Filter(new() { HasText = "Subscriptions" }).GetByRole(AriaRole.Button);
    public ILocator ActiveButtonsLocator => _page.Locator("button.btn.active");
    public ILocator GettingStartedSignupLocator => _page.GetByRole(AriaRole.Heading, new() { Name = "Getting started with Taxually" });
}