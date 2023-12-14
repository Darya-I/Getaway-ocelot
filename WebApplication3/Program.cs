using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Services;

var builder = WebApplication.CreateBuilder(args);

// ��������� ����������� � ���� ������
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ���������� ������� � ��� ���������� � ��������� �����
builder.Services.AddScoped<IProjectService, ProjectService>();

// ���������� ������������
builder.Services.AddControllers();

// ���������� ��������� ������������ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Project", Version = "v1" });
});



var app = builder.Build();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// ��������� ��������� ��������� HTTP-�������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project V1");
    });

}

// ��������� �������� HTTPS
app.UseHttpsRedirection();

// ����������� ��������� ��� ������������
app.UseAuthorization();
app.MapControllers();

app.Run();
