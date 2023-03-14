using System;
using System.Diagnostics;
using System.IO;

namespace ReverseBinaryFile
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                string inputFileName = args[0];
                string outputFileName = args[1];
                Stream reader = File.Open(inputFileName, FileMode.Open);
                Stream writer = File.Open(outputFileName, FileMode.Create);

                byte[] buffer = new byte[8192];
                int read;

                writer.SetLength(reader.Length);
                writer.Seek(0, SeekOrigin.End);
                while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.Seek(-read, SeekOrigin.Current);
                    Array.Reverse(buffer);
                    if (read == buffer.Length)
                    {
                        writer.Write(buffer, 0, read);
                    }
                    else
                    {
                        writer.Write(buffer, buffer.Length - read, read);
                    }
                    writer.Seek(-read, SeekOrigin.Current);
                    Console.Write("\r" + (int)((float)reader.Position / (float)reader.Length * 100) + "% | " + stopwatch.Elapsed);
                }
                reader.Close();
                writer.Close();
                reader.Dispose();
                writer.Dispose();
                stopwatch.Stop();
                Console.Write("\r" + "Done | " + stopwatch.Elapsed);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("\r" + "File name is zero-length, contains only spaces, or contains one or more invalid characters");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("\r" + "The specified path, filename, or both, exceeds the system's maximum length");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("\r" + "Invalid path specified");
            }
            catch (IOException)
            {
                Console.WriteLine("\r" + "An I/O error occurred");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("\r" + "Unauthorized access error occurred while opening the file");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("\r" + "The filename is in an invalid format");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("\r" + "File name was not specified");
            }
            catch(Exception)
            {
                Console.WriteLine("\r" + "Error");
            }
        }
    }
}
