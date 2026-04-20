using Microsoft.EntityFrameworkCore;
using SewControl.API.Middlewares;
using SewControl.Application.MappingProfile;
using SewControl.Application.Services;
using SewControl.Infrastructure.Repositories;
using SewControl.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SewControlContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<SewControlContext>());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<EncargoService>();
builder.Services.AddScoped<UsuariosService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<SewControlMappingProfile>();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();