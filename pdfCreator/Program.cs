using System;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PresentationCore;


namespace pdfCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        static void Run()
        {
            generatePdf(getLocation(), "test");
        }

        /// <summary>
        /// Prompts the user to enter the files location
        /// </summary>
        /// <returns> Location of the file as string </returns>
        public static string getLocation()
        {
            System.Console.WriteLine("Welcome to pdfCreator. \n Please enter the location of the .txt file.");
            string location = Console.ReadLine();
            return location;
        }


        static void generatePdf(string location, string name)
        {
            try
            {
                using (StreamReader sr = new StreamReader(location))
                {
                    string line;
                    int lineCounter = 0;
                    int lineThreshold = 50;

                    PdfDocument pdf = new PdfDocument();
                    pdf.Info.Title = name;
                    // add first page to pdf document
                    PdfPage page = pdf.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont font = new XFont("Verdana", 20, XFontStyle.Bold);


                    while ((line = sr.ReadLine()) != null)
                    {
                        //iterate over all lines in document
                        lineCounter++;

                        gfx.DrawString(line, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height));


                        if (lineCounter > lineThreshold)
                        {
                            // create new page

                        }
                    }

                    pdf.Save(location);
                    


                }
            }
            catch (FileNotFoundException e)
            {
                // start program again when false input was made
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Please enter a valid file location");
                Run();

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

    }
}
