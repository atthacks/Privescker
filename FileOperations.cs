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
                else
                {
                    // See TODO note below why we delete
                    DeletePrivesckerFolder(folderPath);
                }

                // TODO: Improve the way it extracts files. Current method has no ability to overwrite
                ZipFile.ExtractToDirectory(zipPath, folderPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Used to delete all contents of privescker before extracting
        /// </summary>
        /// <param name="folderPath"></param>
        private static void DeletePrivesckerFolder(string folderPath)
        {
            try
            {
                Directory.Delete(folderPath, true);
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
