using System;
using System.IO;

namespace ReverseBinaryFile
{
    class Program
    {
        static void Main(string[] args)
        {
            int bytesToRead = 32768;
            string inputFileName = args[0];
            string outputFileName = args[1];

            BinaryReader reader = new BinaryReader(File.Open(inputFileName, FileMode.Open));
            BinaryWriter writer = new BinaryWriter(File.Open(outputFileName, FileMode.Create));

            byte[] buffer;
            long numBytes = new FileInfo(inputFileName).Length;
            long bytesRead = 0;
            long readOffset = bytesToRead;
            while (bytesRead < numBytes)
            {
                if (readOffset <= numBytes)
                {
                    reader.BaseStream.Seek(-readOffset, SeekOrigin.End);
                    buffer = reader.ReadBytes(bytesToRead);
                    bytesRead += buffer.Length;
                    readOffset += buffer.Length;
                }
                else
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    buffer = reader.ReadBytes((int)(numBytes - bytesRead));
                    bytesRead += buffer.Length;
                }
                Array.Reverse(buffer);
                writer.BaseStream.Seek(0, SeekOrigin.End);
                writer.Write(buffer);
                Console.Write("\r" + bytesRead + " / " + numBytes + " bytes");
            }
            Console.WriteLine("Done");
        }
    }
}
