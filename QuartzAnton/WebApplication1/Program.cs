using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

builder.Services.AddSingleton<CurrencyJob>();
builder.Services.AddSingleton(new JobSchedule(
    jobtype: typeof(CurrencyJob),
    cronExpression: "0/1 * * ? * * * "));
builder.Services.AddHostedService<QuartsHostedServices>();

/*builder.Services.AddHttpClient<ICatalogService, CatalogService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["https://localhost:7284/api/Broker"]);
})
    .AddPolicyHandler(GetRetryPolicy())
    .AddPolicyHandler(GetCircuitBreakerPolicy());*/

var app = builder.Build();



//app.MapGet("/", () => "Hello World!");

app.Run();


