using PdfGenerator.Services.PdfService;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddSingleton<PdfService>();

var app = builder.Build();
using var pdfService = app.Services.GetService<PdfService>()!;

app.MapGet("/ping", () => 
{
    return Results.Ok("pong");
});

app.MapPost("/generate", (PdfRequest input) => 
{
    if (string.IsNullOrEmpty(input.Html))
    {
        return Results.BadRequest("PDF Generator error: HTML missing");
    }

    var pdf = pdfService.GeneratePdf(input.Html);
    return Results.Ok(pdf);
}).RequireAuthorization();

app.MapPost("/download", (PdfRequest input) => 
{
    if (string.IsNullOrEmpty(input.Html))
    {
        return Results.BadRequest("PDF Generator error: HTML missing");
    }

    var pdf = pdfService.GeneratePdf(input.Html);
    return Results.File(pdf, "application/pdf", "GeneratedPdf.pdf");
}).RequireAuthorization();

app.Run();