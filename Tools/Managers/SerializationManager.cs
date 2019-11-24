using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Notes.Tools.Managers
{
    internal static class SerializationManager
    {
        /// <summary>
        /// Serialize obj to file with filePath. If file exists, delete it first.
        /// </summary>
        /// <typeparam name="TObject">type of object to serialize</typeparam>
        /// <param name="obj">object to serialize</param>
        /// <param name="filePath">file path</param>
        internal static void Serialize<TObject>(TObject obj, string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);
                if (file.CreateFolderAndCheckFileExistance())
                {
                    file.Delete();
                }
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formatter.Serialize(stream, obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to serialize data to file {filePath}", ex);
            }
        }

        /// <summary>
        /// Deserialize object from file.
        /// </summary>
        /// <typeparam name="TObject">type of object to deserialize</typeparam>
        /// <param name="filePath"></param>
        /// <returns>deserialized object</returns>
        internal static TObject Deserialize<TObject>(string filePath) where TObject : class
        {
            try
            {
                if (!FileFolderHelper.CreateFolderAndCheckFileExistance(filePath))
                    throw new FileNotFoundException("File doesn't exist.");
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    return stream.Length == 0 ? null : (TObject)formatter.Deserialize(stream);
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Failed to Deserialize Data From File {filePath}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Deserialize Data From File {filePath}", ex);
            }
        }
    }
}
