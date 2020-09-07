using System;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Drawing;



namespace pdfCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Daniel\Documents\vs code\myProjects\test.txt";
            Run(path);
        }

        static void Run(string path = "")
        {
            if (path.Equals(""))
            {
                generatePdf(getLocation(), "test");
            }
            else{
                generatePdf(path, "test");
            }
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
                    //XFont font = new XFont("Verdana", 8, XFontStyle.Bold); //! tweak font size
                    XFont font = new XFont("Monospace", 10, XFontStyle.Regular);
                    // line position x axis
                    int xLinePostition = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        //iterate over all lines in document
                        lineCounter++;

                        gfx.DrawString(line, font, XBrushes.Black, new XRect(0, xLinePostition, page.Width, page.Height), XStringFormats.TopLeft);
                        xLinePostition += 10;

                        if (lineCounter > lineThreshold)
                        {
                            // create new page

                        }
                    }

                    pdf.Save(@"C:\Users\Daniel\Documents\vs code\myProjects\file.pdf");



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
