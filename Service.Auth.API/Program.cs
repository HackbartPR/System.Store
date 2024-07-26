using Service.Auth.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSettings(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddMediatrService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

ApplicationService.ApplyMigrations(app.Services);
await app.RunAsync();
