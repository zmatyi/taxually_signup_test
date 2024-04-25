using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using RecruitmentUiTest.Configuration;

namespace RecruitmentUiTest;

[TestFixture]
public class GlobalSetup : PageTest
{
    private IBrowserContext? _browserContect;
    private IPage? _page;
    private IBrowser? _browser;

    protected IPage PageInTest { get => _page ?? throw new NullReferenceException("Page couldn't be found");}
    
    [SetUp]
    public async Task SetUpUITest()
    {
        await Configure();
        await OpenInitialPage();
    }

    [TearDown]
    public async Task TearDownUITest()
    {
        await _browserContect!.Tracing.StopAsync(new()
        {
            Path = "trace.zip"
        });
    }

    private async Task Configure()
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        BrowserTypeLaunchOptions browserTypeLaunchOptions = new()
        {
            //Setup parameters for presentation
            Headless = false,
            SlowMo = 500
        };

        await ChooseBrowser(playwright, browserTypeLaunchOptions);

        _browserContect = await _browser!.NewContextAsync();

        await CreateTracing();

        _page = await _browserContect.NewPageAsync();
    }

    private async Task CreateTracing()
    {
        await _browserContect!.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    private async Task ChooseBrowser(IPlaywright playwright, BrowserTypeLaunchOptions? browserTypeLaunchOptions = null)
    {
        switch(ConfigManager.GetConfig().BrowserType)
        {
            case BrowserEnumType.Chrome:
                _browser = await playwright.Chromium.LaunchAsync(browserTypeLaunchOptions);
                break;
            case BrowserEnumType.Firefox:
                _browser = await playwright.Firefox.LaunchAsync(browserTypeLaunchOptions);
                break;
            case BrowserEnumType.Webkit:
                _browser = await playwright.Webkit.LaunchAsync(browserTypeLaunchOptions);
                break;
            case BrowserEnumType.Edge:
                browserTypeLaunchOptions ??= new BrowserTypeLaunchOptions();
                browserTypeLaunchOptions.Channel = "msedge";
                _browser = await playwright.Chromium.LaunchAsync(browserTypeLaunchOptions);
                break;
        }

    }
    private async Task OpenInitialPage()
    {
        await PageInTest.GotoAsync(ConfigManager.GetConfig().BaseAddress!);
    }
}