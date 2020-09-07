using System;
using PdfSharp;


namespace pdfCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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

    }
}
