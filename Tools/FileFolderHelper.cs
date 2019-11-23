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

        internal static bool CreateFolderAndCheckFileExistance(string filePath)
        {
            var file = new FileInfo(filePath);
            return file.CreateFolderAndCheckFileExistance();
        }

        internal static bool CreateFolderAndCheckFileExistance(this FileInfo file)
        {
            if (!file.Directory.Exists)
            {
                Console.WriteLine("file directory does not exist");
                file.Directory.Create();
            }
            return file.Exists;
        }
    }
}