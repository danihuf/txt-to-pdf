using System;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Drawing;
using System.Text.RegularExpressions;


namespace pdfCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        /// <summary>
        /// Runs the program
        /// </summary>
        static void Run()
        {
            ConsoleKeyInfo input;
            do
            {
                string inputLoc = getInputLocation();
                string name = getOutputName();
                string outputLoc = getOutputLocation();
                generatePdf(inputLoc, outputLoc, name);

                System.Console.WriteLine("=====================================================");
                System.Console.WriteLine("Press ESC to exit or any key to generate another pdf");
                System.Console.WriteLine("=====================================================");
                input = Console.ReadKey();
            } while (input.Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Prompts the user to enter the files location
        /// </summary>
        /// <returns> Location of the file as string </returns>
        static string getInputLocation()
        {
            System.Console.WriteLine("Welcome to pdfCreator. \nPlease enter the location of the .txt file.");
            string inputLoc = Console.ReadLine();
            if (File.Exists(inputLoc))
            {
                return inputLoc;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("==============================");
                System.Console.WriteLine("Please enter a valid location");
                System.Console.WriteLine("==============================");
                Console.ResetColor();
            }
            return getInputLocation();
        }

        static string getOutputName()
        {
            System.Console.WriteLine("Please enter a name for the pdf file");
            string outputName = Console.ReadLine();
            if (IsValidFilename(outputName))
            {
                return outputName;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("==============================");
                System.Console.WriteLine("Please enter a valid name.\nDon't include: < > : \" / \\ | ? * ");
                System.Console.WriteLine("==============================");
                Console.ResetColor();
            }
            return getOutputName();
        }

        static string getOutputLocation()
        {
            System.Console.WriteLine("Where do you want to save the pdf file? Please input the full path.");
            string outputLoc = Console.ReadLine();
            if (Directory.Exists(outputLoc))
            {
                return outputLoc;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("==============================");
                System.Console.WriteLine("Please enter a valid location");
                System.Console.WriteLine("==============================");
                Console.ResetColor();
            }
            return getOutputLocation();
        }

        /// <summary>
        /// Checks if the given string is a valid file name for windows.
        /// </summary>
        /// <param name="testName"> The name to test </param>
        /// <returns></returns>
        static bool IsValidFilename(string testName)
        {
            Regex containsABadCharacter = new Regex("["
                + Regex.Escape(new string(System.IO.Path.GetInvalidPathChars()))
                + "]");
            if (containsABadCharacter.IsMatch(testName)) { return false; };
            return true;
        }


        /// <summary>
        /// Function generates a pdf document containing the content of a text file.
        /// </summary>
        /// <param name="inputLocation"> The text files location </param>
        /// <param name="outputLocation"> The pdf's location. File gets saved here. </param>
        /// <param name="name"> The pdf's name </param>
        static void generatePdf(string inputLocation, string outputLocation, string name)
        {
            try
            {
                using (StreamReader sr = new StreamReader(inputLocation))
                {
                    string line;
                    int lineCounter = 0;
                    int lineThreshold = 80; // max amount of lines on one page (fontsize: 10)

                    PdfDocument pdf = new PdfDocument();
                    pdf.Info.Title = name;
                    // add first page to pdf document
                    PdfPage page = pdf.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont font = new XFont("Monospace", 10, XFontStyle.Regular); //! font not changing
                    // line position x axis
                    int xLinePostition = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        //iterate over all lines in document
                        lineCounter++;

                        gfx.DrawString($"{line}", font, XBrushes.Black, new XRect(0, xLinePostition, page.Width, 0), XStringFormats.Default);
                        xLinePostition += 10;

                        if (lineCounter > lineThreshold)
                        {
                            // create new page and append to document
                            page = pdf.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            lineCounter = 0;
                            xLinePostition = 0;
                        }
                    }

                    pdf.Save($"{outputLocation}{name}.pdf");
                    System.Console.WriteLine($"PDF successfully created. You can find our file at: {outputLocation}");


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
