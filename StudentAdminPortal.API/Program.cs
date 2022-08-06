using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("StudentAdminPortalDb");

builder.Services.AddDbContext<StudentAdminContext>(options => 
    options.UseSqlServer(connectionString));

// Inject dependencies inside the service - giving an implementation of SqlStudentRespository when called
builder.Services.AddScoped<IStudentRepository, SqlStudentRespository>();
builder.Services.AddScoped<IImageRepository, LocalStorageProfileImageRepository>();

// The following will search for the assembly name (= current application),
// and then search for all automapper profiles, that have inherited from the Profile class,
// resulting in the creation of the maps as specified in AutoMapperProfiles.cs
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Create CORS policy and add it to the services
builder.Services.AddCors((options) =>
{
    options.AddPolicy("angularApplication", (builder) =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .WithMethods("GET", "POST", "PUT", "DELETE")
        .WithExposedHeaders("*");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS policy
app.UseCors("angularApplication");

app.UseAuthorization();

app.MapControllers();

app.Run();
