using Calculator.Library;
using Calculator.Library.Configuration;
using Calculator.Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
if (!File.Exists(configPath))
    throw new FileNotFoundException("appsettings.json не найден!", configPath);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    var services = new ServiceCollection();

    services.AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.AddSerilog(Log.Logger);
    });

    services.Configure<CalculatorOptions>(
        configuration.GetSection(CalculatorOptions.SectionName)
    );

    services.AddScoped<ICalculatorService, CalculatorService>();

    using var provider = services.BuildServiceProvider();

    var calc = provider.GetRequiredService<ICalculatorService>();

    Log.Information("✅ 10 + 2 = {Result}", calc.Add(10, 2));
    Log.Information("✅ 10 * 2 = {Result}", calc.Multiply(10, 2));
    Log.Information("✅ 10 - 2 = {Result}", calc.Subtract(10, 2));
    Log.Information("✅ 10 / 2 = {Result}", calc.Divide(10, 2));
    Log.Information("✅ √16 = {Result}", calc.SquareRoot(16));
    Log.Information("✅ 5 ** 2 = {Result}", calc.Power(5, 2));

    // исключения при maxValue 30
    //Log.Information("✅ 29 + 2 = {Result}", calc.Add(29, 2));
    //Log.Information("✅ 10 / 0 = {Result}", calc.Divide(10, 0));
    //Log.Information("✅ 10 - 12 = {Result}", calc.Subtract(10, 12));
}
catch (Exception ex)
{
    Log.Fatal(ex, "Ошибка");
}
finally
{
    Log.CloseAndFlush();
    Console.ReadKey();
}