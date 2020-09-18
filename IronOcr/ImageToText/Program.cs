using IronOcr;
using System;

namespace ImageToText
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            OneLiner();
            Simple();
            FixLowQualityInput();
            TuneForSpeed();
            ContextAreaRectangles();
            LanguagesArabic();
            Barcodes();
            OcrResultObjectModel();
        }

        private static void OneLiner()
        {
            var Result = new IronTesseract().Read(@"img\Screenshot.png");
            Console.WriteLine(Result.Text);
        }

        private static void Simple()
        {
            var Ocr = new IronTesseract();

            using (var Input = new OcrInput(@"img\Potter.tiff"))
            {
                var Result = Ocr.Read(Input);
                Console.WriteLine(Result.Text);
            }
        }

        private static void FixLowQualityInput()
        {
            var Ocr = new IronTesseract();

            using (var Input = new OcrInput(@"img\Potter.LowQuality.tiff"))
            {
                Input.Deskew();  // removes rotation and perspective
                var Result = Ocr.Read(Input);
                Console.WriteLine(Result.Text);
                Utils.Accuracy.Compare(Result, "txt/Potter.txt");
            }
        }

        private static void TuneForSpeed()
        {
            // Configure for speed.  35% faster and only 0.2% loss of accuracy
            var Ocr = new IronTesseract();
            Ocr.Configuration.BlackListCharacters = "~`$#^*_}{][|\\@¢©«»°±·×‑–—‘’“”•…′″€™←↑→↓↔⇄⇒∅∼≅≈≠≤≥≪≫⌁⌘○◔◑◕●☐☑☒☕☮☯☺♡⚓✓✰";
            Ocr.Configuration.PageSegmentationMode = TesseractPageSegmentationMode.Auto;
            Ocr.Configuration.TesseractVersion = TesseractVersion.Tesseract5;
            Ocr.Configuration.EngineMode = TesseractEngineMode.LstmOnly;
            Ocr.Configuration.ReadBarCodes = false;

            Ocr.Language = OcrLanguage.EnglishFast;

            using (var Input = new OcrInput(@"img\Potter.tiff"))
            {
                var Result = Ocr.Read(Input);
                Console.WriteLine(Result.Text);
                Utils.Accuracy.Compare(Result, "txt/Potter.txt");
            }
        }

        private static void ContextAreaRectangles()
        {
            var Ocr = new IronTesseract();

            using (var Input = new OcrInput())
            {
                // a 41% improvement on speed
                var ContentArea = new System.Drawing.Rectangle() { X = 215, Y = 1250, Height = 280, Width = 1335 };
                Input.AddImage("img/ComSci.png", ContentArea);

                var Result = Ocr.Read(Input);
                Console.WriteLine(Result.Text);
            }
        }

        private static void LanguagesArabic()
        {
            // PM> Install IronOcr.Languages.Arabic

            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Arabic;

            using (var input = new OcrInput())
            {
                input.AddImage("img/arabic.gif");
                // Add image filters if needed
                // In this case, even thought input is very low quality
                // IronTesseract can read what conventional Tesseract cannot.

                var Result = Ocr.Read(input);

                // Console can't print Arabic on Windows easily.
                // Let's save to disk instead.
                Result.SaveAsTextFile("arabic.txt");
            }
        }

        private static void Barcodes()
        {
            var Ocr = new IronTesseract();
            Ocr.Configuration.ReadBarCodes = true;

            using (var input = new OcrInput())
            {
                input.AddImage("img/Barcode.png");

                var Result = Ocr.Read(input);

                foreach (var Barcode in Result.Barcodes)
                {
                    Console.WriteLine(Barcode.Value);
                    // type and location properties also exposed
                }
            }
        }

        private static void OcrResultObjectModel()
        {
            // We can delve deep into OCR results as an object model of
            // Pages, Barcodes, Paragraphs, Lines, Words and Characters

            // This allows us to expore, export and draw OCR content using other APIs/

            var Ocr = new IronTesseract();
            Ocr.Configuration.EngineMode = TesseractEngineMode.TesseractAndLstm;
            Ocr.Configuration.ReadBarCodes = true;

            using (var Input = new OcrInput(@"img\Potter.tiff"))
            {
                OcrResult Result = Ocr.Read(Input);

                foreach (var Page in Result.Pages)
                {
                    // Page object
                    int PageNumber = Page.PageNumber;
                    string PageText = Page.Text;
                    int PageWordCount = Page.WordCount;

                    // null if we dont set Ocr.Configuration.ReadBarCodes = true;
                    OcrResult.Barcode[] Barcodes = Page.Barcodes;

                    System.Drawing.Bitmap PageImage = Page.ToBitmap(Input);
                    int PageWidth = Page.Width;
                    int PageHeight = Page.Height;
                    foreach (var Paragraph in Page.Paragraphs)
                    {
                        // Pages -> Paragraphs
                        int ParagraphNumber = Paragraph.ParagraphNumber;
                        String ParagraphText = Paragraph.Text;
                        System.Drawing.Bitmap ParagraphImage = Paragraph.ToBitmap(Input);
                        int ParagraphX_location = Paragraph.X;
                        int ParagraphY_location = Paragraph.Y;
                        int ParagraphWidth = Paragraph.Width;
                        int ParagraphHeight = Paragraph.Height;
                        double ParagraphOcrAccuracy = Paragraph.Confidence;

                        OcrResult.TextFlow paragrapthText_direction = Paragraph.TextDirection;

                        foreach (var Line in Paragraph.Lines)
                        {
                            // Pages -> Paragraphs -> Lines
                            int LineNumber = Line.LineNumber;
                            String LineText = Line.Text;
                            System.Drawing.Bitmap LineImage = Line.ToBitmap(Input); ;
                            int LineX_location = Line.X;
                            int LineY_location = Line.Y;
                            int LineWidth = Line.Width;
                            int LineHeight = Line.Height;
                            double LineOcrAccuracy = Line.Confidence;
                            double LineSkew = Line.BaselineAngle;
                            double LineOffset = Line.BaselineOffset;
                            foreach (var Word in Line.Words)
                            {
                                // Pages -> Paragraphs -> Lines -> Words
                                int WordNumber = Word.WordNumber;
                                String WordText = Word.Text;
                                System.Drawing.Image WordImage = Word.ToBitmap(Input);
                                int WordX_location = Word.X;
                                int WordY_location = Word.Y;
                                int WordWidth = Word.Width;
                                int WordHeight = Word.Height;
                                double WordOcrAccuracy = Word.Confidence;

                                if (Word.Font != null)
                                {
                                    // Word.Font is only set when using Tesseract Engine Modes rather than LTSM
                                    String FontName = Word.Font.FontName;
                                    double FontSize = Word.Font.FontSize;
                                    bool IsBold = Word.Font.IsBold;
                                    bool IsFixedWidth = Word.Font.IsFixedWidth;
                                    bool IsItalic = Word.Font.IsItalic;
                                    bool IsSerif = Word.Font.IsSerif;
                                    bool IsUnderLined = Word.Font.IsUnderlined;
                                    bool IsFancy = Word.Font.IsCaligraphic;
                                }
                                foreach (var Character in Word.Characters)
                                {
                                    // Pages -> Paragraphs -> Lines -> Words -> Characters
                                    int CharacterNumber = Character.CharacterNumber;
                                    String CharacterText = Character.Text;
                                    System.Drawing.Bitmap CharacterImage = Character.ToBitmap(Input);
                                    int CharacterX_location = Character.X;
                                    int CharacterY_location = Character.Y;
                                    int CharacterWidth = Character.Width;
                                    int CharacterHeight = Character.Height;
                                    double CharacterOcrAccuracy = Character.Confidence;

                                    // Output alternative symbols choices and their probability.
                                    // Very useful for spellchecking
                                    OcrResult.Choice[] Choices = Character.Choices;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}