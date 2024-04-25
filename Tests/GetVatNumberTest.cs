using RecruitmentUiTest.Configuration;
using RecruitmentUiTest.Pages;
using RecruitmentUiTest.Utilities;

namespace RecruitmentUiTest.Tests;

public class GetVatNumberTest : GlobalSetup
{
    private readonly string businessLocationCountry = ConfigManager.GetConfig().BusinessLocationCountry!;
    private readonly string[] countriesAvailable = ["Czech Republic", "France","Germany","Italy","Poland","Spain","EU - IOSS","EU - OSS","United Kingdom"];
    private List<string>? selectedCountries;
    private LandingPage? _landingPage;
    private SignUpPage? _signUpPage;

    [SetUp]
    public void SetUpGetVatNumberTest()
    {
        _landingPage = new LandingPage(PageInTest);
        _signUpPage = new SignUpPage(PageInTest);
    }

    [Test]
    public async Task GetVatNumber()
    {
        
        await _landingPage!.EmailInputLocator.FillAsync(ConfigManager.GetConfig().EmailAddress ?? throw new ArgumentNullException("Email address is not configured"));
        await _landingPage!.PasswordInputLocator.FillAsync(ConfigManager.GetConfig().Password ?? throw new ArgumentNullException("Password is not configured"));
        await _landingPage!.SubmitButtonLocator.ClickAsync();
        await Expect(_signUpPage!.GettingStartedSignupLocator).ToBeVisibleAsync();

        await SelectBusinessLocation();
        await DisableActiveButtons();

        selectedCountries = await SelectTargetCountriesAndVerify(ConfigManager.GetConfig().NumberOfVatCountries);
        await SelectVatNumberForSelectedCountries(selectedCountries);
        await ClickNextButton();
        await FillCreateAccountElements();
        await ClickNextButton();
    }

    private async Task DisableActiveButtons()
    {
        var numberOfActiveButtons = await _signUpPage!.ActiveButtonsLocator.CountAsync();
        for(int i = 0; i< numberOfActiveButtons; i++)
        {
            await _signUpPage!.ActiveButtonsLocator.Last.ClickAsync();
        }
    }

    private async Task SelectBusinessLocation()
    {
        await _signUpPage!.OpenBusinessLocationDropDownLocator().ClickAsync();
        await _signUpPage!.SelectBusinessLocationSelectorLocator(businessLocationCountry).ClickAsync();
    }

    private async Task ClickNextButton()
    {
        await Expect(_signUpPage!.NextStepLocator).ToBeEnabledAsync();
        await _signUpPage!.NextStepLocator.ClickAsync();
    }

    private async Task FillCreateAccountElements()
    {
        await FillAllTextBoxByName();
    }

    private async Task FillAllTextBoxByName()
    {
        await _signUpPage!.GetByLabelLocator("What is your legal status?").ClickAsync();
        await _signUpPage!.OptionRoleLocator("Company").ClickAsync();

        await _signUpPage!.CompanyLegalNameOfBusiness.ClickAsync();
        await _signUpPage!.CompanyLegalNameOfBusiness.FillAsync("MatyasZoltanka_TestCompany" + DateTime.Now);

        await _signUpPage!.GetByLabelLocator("Incorporation number").FillAsync((new ThreadSafeRandom().Next(100) + 1 ).ToString());

        await _signUpPage!.GetByPlaceholderLocator("YYYY-MM-DD").ClickAsync();
        await _signUpPage!.GetByExactTextLocator(DateTime.Now.Day.ToString()).ClickAsync();

        await _signUpPage!.GetByLabelLocator("State").FillAsync("SomeData");

        await _signUpPage!.GetByLabelLocator("ZIP/Post code").FillAsync(new ThreadSafeRandom().Next(1000, 10000).ToString());
        await _signUpPage!.GetByLabelLocator("City").FillAsync("SomeCity");
        await _signUpPage!.GetByLabelLocator("Street").FillAsync("SomeStreet");
        await _signUpPage!.GetByLabelLocator("House number").FillAsync(new ThreadSafeRandom().Next(100, 1000).ToString());
    }

    private async Task SelectVatNumberForSelectedCountries(List<string> selectedCountries)
    {
        foreach(var country in selectedCountries)
        {
            await _signUpPage!.AddVatNumberLocator(country).ClickAsync();
            await Expect(_signUpPage!.AddVatNumberLocator(country)).Not.ToBeVisibleAsync();
        }
    }

    private async Task<List<string>> SelectTargetCountriesAndVerify(int numberOfCountriesToChoose)
    {
        if(numberOfCountriesToChoose < 1 || numberOfCountriesToChoose > 9)
            throw new ArgumentOutOfRangeException("Number of argmunts must be between 1 and 9");
        
        List<string> selectedCountries = [];

        var randomNumbers = Enumerable.Range(0, countriesAvailable.Length).OrderBy(x => new ThreadSafeRandom().Next()).Take(numberOfCountriesToChoose).ToArray();
        
        for(int i = 0; i < numberOfCountriesToChoose; i++)
        {
            await _signUpPage!.SelectServiceCountryLocator(countriesAvailable[randomNumbers[i]]).ClickAsync();
            await Expect(_signUpPage!.AddVatNumberLocator(countriesAvailable[randomNumbers[i]])).ToBeVisibleAsync();
            selectedCountries.Add(countriesAvailable[randomNumbers[i]]);
        }

        return selectedCountries;
    }
}