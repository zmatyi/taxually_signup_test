using Microsoft.Extensions.Configuration;

namespace RecruitmentUiTest.Configuration;

public static class ConfigManager
{
    private static readonly IConfiguration Configuration;
    static ConfigManager()
    {
        Configuration = new ConfigurationBuilder().AddJsonFile("UI.Config.json").AddUserSecrets<Config>().Build();
    }

    public static Config GetConfig() =>
        new()
        {
            BaseAddress = Configuration.GetValue<string?>("BaseAddress"),
            BrowserType = Configuration.GetValue<BrowserEnumType>("BrowserEnumType"),
            NumberOfVatCountries = Configuration.GetValue<int>("NumberOfVatCountries"),
            BusinessLocationCountry = Configuration.GetValue<string?>("BusinessLocationCountry"),
            EmailAddress = Configuration.GetValue<string?>("EmailAddress"),
            Password = Configuration.GetValue<string?>("Password")
        };
}