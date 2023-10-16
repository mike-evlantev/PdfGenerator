using iText.Html2pdf;

public static class PdfService
{
    public static byte[] CreatePdf(string htmlContent) 
    {        
        byte[] bytes;

        using (var memoryStream = new MemoryStream())
        {
            ConverterProperties converterProperties = new ConverterProperties();
            HtmlConverter.ConvertToPdf(htmlContent, memoryStream, converterProperties);
            bytes = memoryStream.ToArray();
        }

        return bytes;
    }
}