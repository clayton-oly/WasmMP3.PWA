using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using MP3.API.Data;

var builder = WebApplication.CreateBuilder(args);

//Inicialize the firebase app
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("Firebase/mp3-pwa-firebase-adminsdk-fbsvc-c69fa54b22.json")
});

//Configura i SQLite

builder.Services.AddDbContext<MP3DbContext>(options =>
    options.UseSqlite("Data Source=pushnotifications.db"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();


//cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapSwagger();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
