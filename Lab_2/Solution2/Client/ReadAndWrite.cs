using System.IO;
using Google.Protobuf.Collections;
using CsvHelper;

namespace Client
{
    public class Read
    {
        public static string ReadFromCsv(string filePath)
        {
            string result = "";

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                    result += line;
            }

            return result;
        }
    }


    public class Write
    {
        public static void WriteTablesToCsv(string filePath, string message, RepeatedField<string> data)
        {
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine(message);

                foreach (var line in data)
                {
                    foreach (var value in line.Split(';'))
                        file.Write(value + ";");

                    file.WriteLine();
                }

            }
        }


        public static void WritePathSeqToCsv(string filePath, string message, string pathSeq)
        {
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine(message);
                file.WriteLine(pathSeq);
                file.WriteLine();
            }
        }
        
    }
    
}