using Cycode;
using Cycode.BusinessServices;
using Cycode.DataAccess;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Cycode.DataContracts;
using Microsoft.Extensions.Options;
using FluentValidation;
using Cycode.DataContracts.Validators;
using GraphQLParser;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Cycode.BusinessLogic;
using Cycode.BusinessServices.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ScanRequestValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning();
builder.Services.AddHttpClient();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var githubConfig = configuration.GetSection("GithubConfig").Get<GithubConfig>();
var graphqlClient = new GraphQLHttpClient(new GraphQLHttpClientOptions()
{
    EndPoint = new Uri(githubConfig.GraphQLServerUri)
}, new NewtonsoftJsonSerializer());

graphqlClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("GITHUB-ACCESS-TOKEN")}");

builder.Services.AddSingleton<IGraphQLClient>(s => graphqlClient);

builder.Services.AddScoped<ISecurityVulnerabilityService, SecurityVulnerabilityService>();
builder.Services.AddScoped<IScanRequestMapper, ScanRequestMapper>();
builder.Services.AddScoped<ISecurityVulnerabilityRepository, SecurityVulnerabilityRepository>();
builder.Services.AddScoped<IPackageVulnerabilityLogicManager, PackageVulnerabilityLogicManager>();


var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Text.Plain;
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is Exception)
        {
            await context.Response.WriteAsync($"An exception was thrown: {exceptionHandlerPathFeature?.Error.Message}");
        }
    });
});


app.Run();

