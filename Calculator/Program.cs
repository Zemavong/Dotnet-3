

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Calculator.Library;
using Calculator.Library.Services;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Запуск приложения...");

    var services = new ServiceCollection();

    services.AddSingleton<IConfiguration>(configuration);

    services.AddScoped<Calculator.Library.Services.ICalculatorService, CalculatorService>();

    var serviceProvider = services.BuildServiceProvider();

    var calculator = serviceProvider.GetRequiredService<ICalculatorService>();

    string result = calculator.Add(10.00, 5.00).ToString();
    Log.Information("Результат сложения: {Result}", result);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Приложение завершилось с ошибкой");
}
finally
{
    Log.CloseAndFlush();
}
