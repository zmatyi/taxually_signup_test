# RecruitmentUiTest

## How to Use

1. Restore and build the application using the following commands: 
    ```
    dotnet restore
    dotnet build
    ```
2. Install Playwright by executing the `playwright.ps1` script using PowerShell (pwsh) in the `bin/Debug/net8.0` folder.
3. Configure the `UI.Config.json` file to set up your desired test settings:
    ```json
    {
        "BrowserEnumType": "Chrome", // Browser to use. Available browsers: Chrome, Firefox, Webkit, Edge (For more options, see Configuration/BrowserEnumType.cs)
        "BaseAddress": "https://app.taxually.com/", // Address to the Taxually landing page
        "NumberOfVatCountries": 3, // Number of countries to select as target countries
        "BusinessLocationCountry": "United Kingdom", // The country where the business is located
        "EmailAddress": "", // Sensitive data, add your credentials, use user secret to hide your sensitive data
        "Password": "" // Sensitive data, add your credentials, use user secret to hide your sensitive data
    }
    ```
    In Visual Studio you can set user secret in the following way: [In Visual Studio, right-click the project in Solution Explorer, and select Manage User Secrets from the context menu.](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows#use-visual-studio).
4. Execute the tests using the following command:
    ```
    dotnet test
    ```
