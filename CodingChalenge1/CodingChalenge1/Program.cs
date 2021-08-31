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
            try
            {
                var gameNumbers = GetGameNumbers();

                for (;;)
                {
                    var numberRaw = streamReader.ReadLine();
                    var coinCount = int.Parse(numberRaw.Trim());

                    var pile = coinCount;
                    for (;;) 
                    {
                        pile = Pat(gameNumbers, pile);
                        if (pile == 0) { 
                            WritePat(streamWriter);
                            break;
                        }
                        pile = Mat(gameNumbers, pile);
                        if (pile == 0) { 
                            WriteMat(streamWriter);
                            break;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                return;
            }
        }

        private static int Pat(List<int> gameNumbers, int pile) 
        {
            return Play(gameNumbers, pile);
        }

        private static int Mat(List<int> gameNumbers, int pile)
        {
            return Play(gameNumbers, pile);
        }

        private static int Play(List<int> gameNumbers, int pile)
        {
            int levelIn = 0;
            int levelOut;
            int numberToSubtract = FindTheBest(gameNumbers, pile, levelIn, out levelOut);
            pile -= numberToSubtract;
            return pile;
        }

        private static int FindTheBest(List<int> gameNumbers, int pile, int levelIn, out int levelOut)
        {
            levelIn++;
            levelOut = levelIn;
            var bestNumber = 1;
            var lowerNumbers = GetLowerNumbers(gameNumbers, pile);

            var closestNumber = GetClosestNumber(gameNumbers, pile);
            var residue = pile - closestNumber;

            if (residue == 0)
            {
                return closestNumber;
            }

            foreach (var gameNumber in lowerNumbers)
            {
                if (pile == 0)
                {
                    break;
                }
                if (pile > 0)
                {
                    var numberCheckResult = FindTheBest(gameNumbers, residue, levelIn, out levelOut);
                    if (numberCheckResult > bestNumber && levelOut % 2 == 0)
                    {
                        bestNumber = numberCheckResult;
                        break;
                    }
                }
            }

            return bestNumber;
        }

        private static int GetClosestNumber(List<int> gameNumbers, int coinCount)
        {
            var seekedNumber = 0;
            foreach (var gameNumber in gameNumbers)
            {
                if (gameNumber <= coinCount)
                {
                    seekedNumber = gameNumber;
                    continue;
                }
            }
            return seekedNumber;
        }

        private static List<int> GetLowerNumbers(List<int> gameNumbers, int coinCount)
        {
            List<int> lowerNumbers = new List<int>();
            foreach (var gameNumber in gameNumbers)
            {
                if (gameNumber <= coinCount)
                {
                    lowerNumbers.Add(gameNumber);
                    continue;
                }
            }
            return lowerNumbers;
        }

        private static List<int> GetGameNumbers()
        {
            List<int> gameNumbers = new List<int>();
            int maxValue = 10000;
            int counter = 1;
            while (counter <= maxValue)
            {
                var possibleGameNumber = counter * counter;
                if (possibleGameNumber <= maxValue)
                {
                    gameNumbers.Add(possibleGameNumber);
                }

                counter++;
            }
            return gameNumbers;
        }

        private static void WritePat(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("Pat");
        }

        private static void WriteMat(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("Mat");
        }
    }
}
