# Basic Blog Application
A full-stack blog application built with Angular and .NET Core Web API.

## Features
- User authentication and authorization
- CURD operation

## Tech Stack
### Frontend
- Angular 16
- nodejs v18.20.6
- HTML/CSS/TypeScript

### Backend
- .NET Core Web API (net7.0)
- EF Core
- JWT Authentication

### Database
- Microsoft SQL Server

## Project Setup
### Frontend 🅰️
#### Create new project
`ng new project-name`
- run frontend : `ng serve`

#### Import Bootstrap CDN
- click https://getbootstrap.com/
- go to **Include via CDN** and copy link to your angular project in `index.html`
```
<head>
  <!-- Rest are the same -->

  <!-- Add bootstrap css and JS (included via CDN)  -->
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</head>
```

#### create component
- generate component : `ng g c component_name`
- generate service : `ng g s service_name`
- generate auth : `ng generate auth`
  - Which type of guard would you like to create? `CanActivate`

### Backend 👨🏻‍💻
#### Setup connection string and Jwt
```
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "BlogAppConnectionString": "Server=SQL_server_name; Database=database_name; TrustServerCertificate=True; Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "your-very-long-secret-key-here-minimum-32-characters",
    "Issuer": "backend localhost URL", // The service that generates the JWT token
    "Audience": "frontend localhost URL" // The service that receive and use the token
  }
```
#### Add dependency injection in `Program.cs`
- You need to create your own Dbcontent before perform this step
```
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAppConnectionString"));
});
```
#### Nuget package for EF core
```
    Microsoft.EntityFrameworkCore.SqlServer
    Microsoft.EntityFrameworkCore.Tools
```
- Add migration of EF core in VS 2022
```
Tool->Nuget package mamager->package manager console

*** type in pm console***
// single DbContext in project
Add-Migration "Name of Migration"
Update-Database

// multiple DbContext in project
Add-Migration "Name of Migration" -Context "DbContext name"
Update-Database -Context "DbContext name"
```
#### Nuget package for authentication & authorization
```
  Microsoft.AspNetCore.Authentication.JwtBearer
  Microsoft.AspNetCore.Identity.EntityFrameworkCore
  Microsoft.IdentityModel.Tokens
  Microsoft.AspNetCore.Authentication.JwtBearer
```
#### Enable CORS 
- go to `Program.cs`
```
app.UseCors(options =>
{
    options.AllowAnyHeader()
           .WithOrigins("front end localhost URL") 
           .AllowAnyMethod();
});
```
