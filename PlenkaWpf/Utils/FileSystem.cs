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
    /// <summary>
    ///     Класс для работы с файлами
    /// </summary>
    internal static class FileSystem

    {
        private static Image createAndFitImage(byte[] bitmap, Document document)
        {
            var image = new Image(ImageDataFactory.Create(bitmap)).SetTextAlignment(TextAlignment.CENTER);
            fitImageToDocument(image, document);

            return image;
        }

        private static void fitImageToDocument(Image image, Document document)
        {
            var widthscaler =
                (document.GetPageEffectiveArea(PageSize.A4).GetWidth() - document.GetLeftMargin() -
                 document.GetRightMargin()) / image.GetImageWidth();

            var heighscaler =
                (document.GetPageEffectiveArea(PageSize.A4).GetHeight() - document.GetTopMargin() -
                 document.GetBottomMargin()) / image.GetImageHeight();

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
        }

        /// <summary>
        ///     Функция экспорта результатов в пдф
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="tempBitmap">График температуры</param>
        /// <param name="nBitMap">График вязкости</param>
        /// <param name="mathModel">Результаты расчетов и начальные параметры</param>
        public static void
            exportPdf(string path, byte[] tempBitmap, byte[] nBitMap, MathClass mathModel) // todo Переписать
        {
            var results = mathModel.Results;
            var writer = new PdfWriter(path);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            //Trace.WriteLine($"-----------------------------------{Directory.GetCurrentDirectory()}");
            var FONT_FILENAME = "../../../resources/Times_New_Roman.ttf";
            var font = PdfFontFactory.CreateFont(FONT_FILENAME, PdfEncodings.IDENTITY_H);

            var header = new Paragraph("Отчёт о моделировании неизотермического течения аномально-вязкого материала").SetFont(font)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            document.SetFont(font);

            var tGraphImage = createAndFitImage(tempBitmap, document);
            document.Add(header);
            document.Add(new Paragraph("Входные данные"));

            var initialTable = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();

            initialTable.AddCell(new Cell(1, 2).Add(new Paragraph("Геометрические параметры канала").SetTextAlignment(TextAlignment.CENTER)));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Длина, м")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.L.ToString())));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Ширина, м")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.W.ToString())));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Глубина, м")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.H.ToString())));

            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Тип материала")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.MaterialName)));

            initialTable.AddCell(new Cell(1, 2).Add(new Paragraph("Параметры свойств материала").SetTextAlignment(TextAlignment.CENTER)));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Плотность кг/м³")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.p.ToString())));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Удельная теплоёмкость, Дж/(кг·°С)")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.c.ToString())));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Температура плавления, °С")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.T0.ToString())));

            initialTable.AddCell(new Cell(1, 2).Add(new Paragraph("Режимные параметры процесса").SetTextAlignment(TextAlignment.CENTER)));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Скорость движения крышки, м/с")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.Vu.ToString())));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Температура крышки, °С")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.Tu.ToString())));

            initialTable.AddCell(new Cell(1, 2).Add(new Paragraph("Параметры математической модели").SetTextAlignment(TextAlignment.CENTER)));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Шаг расчёта по длине канала, м")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.step.ToString())));

            initialTable.AddCell(new Cell(1, 2).Add(new Paragraph("Эмпирические коэффициенты математической модели")
                                                        .SetTextAlignment(TextAlignment.CENTER)));

            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Коэффициент консистенции при температуре приведения, Па·сⁿ")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.u0.ToString())));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Температурный коэффициент вязкости материала, 1/°С")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.b.ToString())));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Температура приведения, °С")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.Tr.ToString())));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Индекс течения материала")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.n.ToString())));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph("Коэффициент теплоотдачи от крышки канала к материалу, Вт/(м²·°С)")));
            initialTable.AddCell(new Cell(1, 1).Add(new Paragraph(mathModel.cp.au.ToString())));

            document.Add(initialTable);
            document.Add(new AreaBreak());

            document.Add(new Paragraph("График температуры"));
            document.Add(tGraphImage);

            var nGraphImage = createAndFitImage(nBitMap, document);
            document.Add(new AreaBreak());
            document.Add(new Paragraph("График вязкости"));
            document.Add(nGraphImage);

            document.Add(new Paragraph("Критериальные показатели"));
            document.Add(new Paragraph($"Температура продукта {results.T} °С"));
            document.Add(new Paragraph($"Вязкость продукта {results.N} Па·с"));
            document.Add(new Paragraph($"Производительность канала {results.Q} кг/ч"));

            document.Add(new AreaBreak());

            var resultTable = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
            resultTable.AddHeaderCell("Координата по длине канала, м");
            resultTable.AddHeaderCell("Температура, °С");
            resultTable.AddHeaderCell("Вязкость, Па·с");

            for (var i = 0; i < results.cordTempNs.Count; i++)
            {
                resultTable.AddCell(results.cordTempNs[i].cord.ToString());
                resultTable.AddCell(results.cordTempNs[i].n.ToString());
                resultTable.AddCell(results.cordTempNs[i].temp.ToString());
            }

            document.Add(new Paragraph("Таблица параметров состояния"));
            document.Add(resultTable);
            document.Close();
        }
    }
}
