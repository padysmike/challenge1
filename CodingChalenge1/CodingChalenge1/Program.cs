using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace CodingChalenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigLoader.LoadConfig();

            StartWithFile(config.InputFilePath, config.OutputFilePath);
        }

        private static void StartWithFile(string inputTextPath, string outputTextPath)
        {
            using (var streamWriter = new StreamWriter(outputTextPath))
            {
                using (var streamReader = new StreamReader(inputTextPath))
                {
                    StartWithStreams(streamWriter, streamReader);
                    streamReader.Close();
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        private static void StartWithStreams(StreamWriter streamWriter, StreamReader streamReader)
        {
            WriteText(streamWriter);
        }


        private static void WriteText(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("text");
        }

        private static string[] ReadWords(StreamReader streamReader)
        {
            var line = streamReader.ReadLine().Trim().ToLower();
            return line.Split(" ");
        }

        private static int ReadNumber(StreamReader streamReader)
        {
            var testCaseCountRaw = streamReader.ReadLine();
            return int.Parse(testCaseCountRaw.Trim());
        }
    }
}
