using System;
using System.IO;

namespace Notes.Tools
{
    internal static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string AppFolderPath =
            Path.Combine(AppDataPath, "Notes");

        internal static readonly string StorageFilePath =
            Path.Combine(AppFolderPath, "CurrentUser.ser");

        /// <summary>
        /// Create a file folder if it doesn't exist and return whether the file exists.
        /// </summary>
        /// <param name="file">file path</param>
        /// <returns>whether file exists on the specified path</returns>
        internal static bool CreateFolderAndCheckFileExistance(string filePath)
        {
            var file = new FileInfo(filePath);
            return file.CreateFolderAndCheckFileExistance();
        }

        /// <summary>
        /// Create a file folder if it doesn't exist and return whether the file exists.
        /// </summary>
        /// <param name="file">file info</param>
        /// <returns>whether file exists</returns>
        internal static bool CreateFolderAndCheckFileExistance(this FileInfo file)
        {
            if (!file.Directory.Exists)
            {
                // create file directory if it doesn't exist
                file.Directory.Create();
            }
            // return whether the file exists
            return file.Exists;
        }
    }
}