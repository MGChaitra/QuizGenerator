using EducationAPI.Plugins;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(setUp =>
{
    setUp.AddPolicy("cors", setUp =>
    {
        setUp.AllowAnyHeader();
        setUp.AllowAnyMethod();
        setUp.AllowAnyOrigin();
    });
});
// Add services to the container.
IConfigurationRoot configuration = new ConfigurationBuilder()

.AddJsonFile("appsettings.json")

.AddUserSecrets<Program>()

.Build();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton(sp =>
{
  
    var kernelBuilder = Kernel.CreateBuilder();
    kernelBuilder.AddAzureOpenAIChatCompletion(
        deploymentName: configuration["AzureOpenAI:DeploymentName"],
        endpoint: configuration["AzureOpenAI:Endpoint"],
        apiKey: configuration["AzureOpenAI:ApiKey"]);
  
    var quiz = new QuizPlugin(configuration);
    kernelBuilder.Plugins.AddFromObject(quiz, pluginName: "Quiz");
    return kernelBuilder.Build();
});

builder.Services.AddSingleton<IConfiguration>(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("cors");

app.UseAuthorization();

app.MapControllers();

app.Run();
