# api-help-desk

## Project Configuration

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio Code](https://code.visualstudio.com/)

### Setting Up the Project

### Install required NuGet packages:

dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Dapper
dotnet add package System.Data.SqlClient
dotnet add package Swashbuckle.AspNetCore

### Add external DLL references:
<ItemGroup>
  <Reference Include="ThroneConnection">
    <HintPath>lib\ThroneConnection.dll</HintPath>
  </Reference>
</ItemGroup>
<ItemGroup>
  <None Update="lib\ThroneConnection.dll">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>

### Configure JWT Authentication:
builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

### Configure Swagger:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

## Add key jwt

  "Jwt": {
    "Issuer": "WebApiJwt.com",
    "Audience": "localhost",
    "Key": "a5y7YlnBB4thxt5eZAnqo0dv4/Z95rO8WN5Ah+/sQM4="
  },