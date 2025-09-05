using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackBuldTechnicalAssessment.Data;
using StackBuldTechnicalAssessment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://httpstatuses.com/400",
                Title = "Validation errors occurred."
            };
            return new BadRequestObjectResult(problemDetails);
        };
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{ 
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    options.UseSqlServer(connectionString);
});


builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "StackBuld Technical Assessment API";
    options.Version = "v1";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseProblemDetailsHandler();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    SeedData.SeedRuntime(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
