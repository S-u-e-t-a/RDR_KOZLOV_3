using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using PlenkaAPI;


namespace PlenkaWpf.Utils
{
    internal class FileSystem 

    {
        public static void exportPdf(string path, byte[] tempBitmap, byte[] nBitMap, CalculationResults results) // todo Переписать
        {
            var writer = new PdfWriter(path);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            //Trace.WriteLine($"-----------------------------------{Directory.GetCurrentDirectory()}");
            var FONT_FILENAME = "../../../resources/Times_New_Roman.ttf";
            var font = PdfFontFactory.CreateFont(FONT_FILENAME, PdfEncodings.IDENTITY_H);
            var header = new Paragraph("Анализ методов замещения страниц").SetFont(font)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.SetFont(font);
            var image = new Image(ImageDataFactory.Create(tempBitmap)).SetTextAlignment(TextAlignment.CENTER);
            var widthscaler = (document.GetPageEffectiveArea(PageSize.A4).GetWidth() - document.GetLeftMargin() - document.GetRightMargin()) / image.GetImageWidth();
            var heighscaler = (document.GetPageEffectiveArea(PageSize.A4).GetHeight() - document.GetTopMargin() - document.GetBottomMargin()) / image.GetImageHeight();
            float scaler;
            if (widthscaler < heighscaler)
            {
                scaler = widthscaler;
            }
            else
            {
                scaler = heighscaler;
            }

            image.Scale(scaler, scaler);
            document.Add(header);
            document.Add(new Paragraph("График температуры"));
            document.Add(image);

            image = new Image(ImageDataFactory.Create(nBitMap)).SetTextAlignment(TextAlignment.CENTER);
            widthscaler = (document.GetPageEffectiveArea(PageSize.A4).GetWidth() - document.GetLeftMargin() - document.GetRightMargin()) / image.GetImageWidth();
            heighscaler = (document.GetPageEffectiveArea(PageSize.A4).GetHeight() - document.GetTopMargin() - document.GetBottomMargin()) / image.GetImageHeight();
            if (widthscaler < heighscaler)
            {
                scaler = widthscaler;
            }
            else
            {
                scaler = heighscaler;
            }

            image.Scale(scaler, scaler);
            document.Add(new AreaBreak());
            document.Add(new Paragraph("График вязкости"));
            document.Add(image);



            Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
            table.AddHeaderCell("Координата по длине канала, м");
            table.AddHeaderCell("Температура, °С");
            table.AddHeaderCell("Вязкость, Па·с");
            for (int i = 0; i < results.Ti.Count; i++)
            {
                table.AddCell(results.Ni.ElementAt(i).Key.ToString());
                table.AddCell(results.Ni.ElementAt(i).Value.ToString());
                table.AddCell(results.Ti.ElementAt(i).Value.ToString());
            }

            
            document.Add(new Paragraph($"Температура продукта {results.T} °С"));
            document.Add(new Paragraph($"Вязкость продукта {results.N} Па·с"));
            document.Add(new Paragraph($"Производительность канала {results.Q} кг/ч"));

            document.Add(new Paragraph("Таблица значений"));
            document.Add(table);
            document.Close();
        }
    }
}
