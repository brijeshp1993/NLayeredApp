using System;
using System.IO;

namespace Demo.Common.Utils
{
    /// <summary>
    /// The file utility class
    /// This class contains methods to write/read data to/from files
    /// </summary>
    public class FileUtil
    {
        /// <summary>
        /// write text to file, if file exist then overwrite the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        public void WriteTextToFile(string path, string text)
        {
            try
            {
                //     Creates a new file, writes the specified string to the file, and then closes
                //     the file. If the target file already exists, it is overwritten.
                File.WriteAllText(path, text);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// append text to file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        public void AppendTextToFile(string path, string text)
        {
            try
            {
                //     Opens a file, appends the specified string to the file, and then closes the file.
                //     If the file does not exist, this method creates a file, writes the specified
                //     string to the file, then closes the file.
                File.AppendAllText(path, text);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Create new directory with parent folders
        /// </summary>
        /// <param name="path"></param>
        public void CreateDirectory(string path)
        {
            try
            {
                //     Creates all directories and subdirectories in the specified path unless they
                //     already exist.
                Directory.CreateDirectory(path);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// check given path is folder or file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>true if path is for directory, else false</returns>
        public bool IsDirectory(string path)
        {
            try
            {
                return ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete file from given path
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="throwException"></param>
        /// <returns></returns>
        public static bool DeleteFile(string fileName, bool throwException = false)
        {
            var result = false;
            try
            {
                File.Delete(fileName);
                result = true;
            }
            catch (Exception)
            {
                if (throwException) throw;

            }
            return result;
        }
    }
}
