var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-7.0
//https://www.infoworld.com/article/3669188/how-to-implement-jwt-authentication-in-aspnet-core-6.html

app.MapPost("/generate", (GeneratorInput input) => 
{
    if (string.IsNullOrEmpty(input.Html))
    {
        return Results.BadRequest("PDF Generator error: HTML missing");
    }
    var pdf = PdfService.CreatePdf(input.Html);
    //return Results.File(pdf, "application/pdf", "GeneratedPdf.pdf");
    return Results.Ok(pdf);
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status403Forbidden)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/token", (User user) => $"Generation token for user {user.UserName}");

app.Run();