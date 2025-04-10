using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetryAIDemo;
using OpenTelemetryAIDemo.Extensions;
using OpenTelemetryAIDemo.Modules;
using System;
using System.Text;

class Program
{
   static async Task Main(string[] args)
   {
      Console.OutputEncoding = Encoding.UTF8;
      Console.Title = "🤖 OpenTelemetry - Interactive Console";

      var host = Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostingContext, config) =>
           {
              config.AddJsonFile("appsettings.json", optional: true);
              config.AddJsonFile("appsettings.Development.json", optional: false);
           })
           .ConfigureServices((context, services) =>
           {
              var configuration = context.Configuration;
              var serviceName = "OtelDemo";
              var serviceVersion = "1.0.0";

              services.AddOpenTelemetryTelemetry(configuration, serviceName, serviceVersion);

              services.AddSemanticKernel(configuration);

              services.AddTransient<ChatCompletion>();
              services.AddTransient<ChatCompletionWithFunctionCalls>();
              services.AddTransient<MultiAgentInteractions>();

              services.AddSingleton<ModuleFactory>();

              services.AddSingleton<OtelApp>();
           })
           .Build();      

      var app = host.Services.GetRequiredService<OtelApp>();
      await app.Run();

   }
}
