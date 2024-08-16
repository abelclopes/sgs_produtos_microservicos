using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "v1";
    config.Title = "TodoAPI v1";
    config.Version = "v1";
});

builder.Services.AddSwaggerGen();
// Adiciona servi�os de autentica��o e autoriza��o (ajuste conforme necess�rio)
builder.Services.AddAuthentication(options =>
{
    // Configure a autentica��o aqui (exemplo: JWT Bearer)
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Configure as op��es do JWT aqui, como a chave secreta, emissor, etc.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "test",
        ValidAudience = "test",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9d3f504c-d63b-4c71-a2de-0e5e4c8bb048"))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Adiciona autentica��o ao pipeline
app.UseAuthorization();

app.MapControllers();

app.Run();
