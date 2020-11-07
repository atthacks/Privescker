namespace Privescker
{
    using System;
    using System.IO;
    using System.IO.Compression;
    public class FileOperations
    {
        /// <summary>
        /// Unzip the archive file we downloaded
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="folderPath"></param>
        public static void UnzipArchive(string zipPath, string folderPath)
        {
            try
            {
                if (!File.Exists(zipPath))
                {
                    throw new FileNotFoundException();
                }
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(folderPath, entry.FullName));
                        entry.ExtractToFile(destinationPath, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Try and delete the archive file after it has finished extracting
        /// </summary>
        /// <param name="file"></param>
        public static void DeleteArchiveFile(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
