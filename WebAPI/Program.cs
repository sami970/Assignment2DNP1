using AssignmentDNP1.Services;
using Blazor.Services;
using EfcData;
using JsonDataAccess.DataAccess;

using (PostContext ctx = new())
{
    ctx.SeedPost();
    ctx.SeedUser();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Forum
builder.Services.AddScoped<IForumHome, PostDAO>();
builder.Services.AddScoped<PostContext>();

//User
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<PostUserFileContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();