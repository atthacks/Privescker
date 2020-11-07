namespace Privescker
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    public class AppLockerLocations
    {
        /// <summary>
        /// Gets a list of all the applocker bypass locations
        /// </summary>
        /// <returns></returns>
        public List<string> GetAppLockerBypassLocations()
        {
            //List from: https://github.com/api0cradle/UltimateAppLockerByPassList/blob/master/Generic-AppLockerbypasses.md
            var lst = new List<string>();
            lst.Add(@"C:\Windows\Tasks");
            lst.Add(@"C:\Windows\Temp");
            lst.Add(@"C:\Windows\tracing");
            lst.Add(@"C:\Windows\Registration\CRMLog");
            lst.Add(@"C:\Windows\System32\FxsTmp");
            lst.Add(@"C:\Windows\System32\com\dmp");
            lst.Add(@"C:\Windows\System32\Microsoft\Crypto\RSA\MachineKeys");
            lst.Add(@"C:\Windows\System32\spool\PRINTERS");
            lst.Add(@"C:\Windows\System32\spool\SERVERS");
            lst.Add(@"C:\Windows\System32\spool\drivers\color");
            lst.Add(@"C:\Windows\System32\Tasks\Microsoft\Windows\SyncCenter");
            lst.Add(@"C:\Windows\System32\Tasks_Migrated");
            lst.Add(@"C:\Windows\SysWOW64\FxsTmp");
            lst.Add(@"C:\Windows\SysWOW64\com\dmp");
            lst.Add(@"C:\Windows\SysWOW64\Tasks\Microsoft\Windows\SyncCenter");
            lst.Add(@"C:\Windows\SysWOW64\Tasks\Microsoft\Windows\PLA\System");
            lst.Add(@"C:\Users\Public"); //I like this directory
            return lst;
        }

        /// <summary>
        /// Returns the locations to the user when they want to print them out
        /// </summary>
        /// <returns></returns>
        public List<string> GetLocations()
        {
            return GetAppLockerBypassLocations().Where(n => Directory.Exists(n) && HasWriteAccessToFolder(n)).ToList();
        }

        /// <summary>
        /// Checks to see if the current user has write access to the AppLocker bypass locations
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private static bool HasWriteAccessToFolder(string folderPath)
        {
            try
            {
                // Attempt to get a list of security permissions from the folder. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
