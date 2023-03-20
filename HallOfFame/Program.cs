using HallOfFame.Data;
using HallOfFame.Interfaces;
using HallOfFame.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ���������� ��������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ����� ��������� ��� ������� ��
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<PersonsAPIDbContext>(options => options.UseInMemoryDatabase("PersonsDb"));
}
else
{
    builder.Services.AddDbContext<PersonsAPIDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PersonsApiConnectionString")));
}

// ��������� ���������� � �������
builder.Services.AddTransient<IPersonsService, PersonsService>();

var app = builder.Build();

app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.Run();
