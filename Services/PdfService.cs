using iText.Html2pdf;

public static class PdfService
{
    public static byte[] CreatePdf(string htmlContent) 
    {        
        byte[] bytes;

        using (var memoryStream = new MemoryStream())
        {
            HtmlConverter.ConvertToPdf(htmlContent, memoryStream);
            bytes = memoryStream.ToArray();
        }

        return bytes;
    }
}