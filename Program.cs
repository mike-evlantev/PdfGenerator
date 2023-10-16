var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddJwtBearer();

var app = builder.Build();

//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-7.0
//https://www.infoworld.com/article/3669188/how-to-implement-jwt-authentication-in-aspnet-core-6.html

app.MapGet("/ping", () => 
{
    var issuer = builder.Configuration["Authentication:Schemes:Bearer:ValidIssuer"];

    return Results.Ok(issuer);
});

app.MapPost("/generate", (GeneratorInput input) => 
{
    if (string.IsNullOrEmpty(input.Html))
    {
        return Results.BadRequest("PDF Generator error: HTML missing");
    }

    var pdf = PdfService.CreatePdf(input.Html);
    return Results.Ok(pdf);
});

app.MapPost("/download", (GeneratorInput input) => 
{
    if (string.IsNullOrEmpty(input.Html))
    {
        return Results.BadRequest("PDF Generator error: HTML missing");
    }

    var pdf = PdfService.CreatePdf(input.Html);
    return Results.File(pdf, "application/pdf", "GeneratedPdf.pdf");
});

app.MapPost("/token", (User user) => $"Generation token for user {user.Username}");

app.Run();