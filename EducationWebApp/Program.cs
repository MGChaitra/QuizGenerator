using EducationWebApp.Services;
using EducationWebApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System;
using EducationWebApp.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IOpenAIService,OpenAIService>();
builder.Services.AddScoped<IGenerateQuizPDFContentService,GenerateQuizPDFContentService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7291/") });

await builder.Build().RunAsync();
