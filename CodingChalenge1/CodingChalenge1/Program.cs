using System.IO;

namespace CodingChalenge1
{
    class Program
    {


        static void Main(string[] args)
        {
            var config = ConfigLoader.LoadConfig();

            var textInput = ReadText(config.InputFilePath);

            WriteText(config.OutputFilePath, textInput);
        }

        private static void WriteText(string outputTextPath, string textOutput)
        {
            using (var streamWriter = new StreamWriter(outputTextPath))
            {
                streamWriter.Write(textOutput);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        private static string ReadText(string inputTextPath)
        {
            string textInput;
            using (var streamReader = new StreamReader(inputTextPath))
            {
                textInput = streamReader.ReadToEnd();
                streamReader.Close();
            }

            return textInput;
        }
    }
}
