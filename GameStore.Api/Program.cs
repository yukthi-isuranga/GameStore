using System.Runtime.CompilerServices;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

app.MapGameEndpoints();

app.Run();
