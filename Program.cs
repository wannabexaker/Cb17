using CB17.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===================================================
//  DATABASE CONFIGURATION
// ===================================================

// Register Entity Framework Core with PostgreSQL provider
// Reads the connection string from appsettings.json ("DefaultConnection")
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ===================================================
//  SERVICES CONFIGURATION
// ===================================================

// Adds controller support (for API endpoints using MVC pattern)
builder.Services.AddControllers();

// Adds minimal endpoint metadata for Swagger generation
builder.Services.AddEndpointsApiExplorer();

// Registers Swagger service to auto-generate API documentation and UI
builder.Services.AddSwaggerGen();

// ===================================================
//  APPLICATION PIPELINE
// ===================================================

var app = builder.Build();

// Create a scope and run the initial data seeding logic
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initialize(db);
}


// In Development mode → enable Swagger UI for testing endpoints
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        // Generates the OpenAPI JSON
    app.UseSwaggerUI();      // Provides the web UI at /swagger
}

// Enforces HTTPS for all requests (redirects HTTP → HTTPS)
app.UseHttpsRedirection();

// Maps controller endpoints to routes like api/controller
app.MapControllers();

// Starts the web application
app.Run();
