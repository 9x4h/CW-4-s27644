using Microsoft.EntityFrameworkCore;
using zad3_apbd;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));

builder.Services.AddScoped<PrescriptionService>();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();