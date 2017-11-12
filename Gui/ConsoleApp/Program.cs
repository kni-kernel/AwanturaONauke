using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwanturaLib;
using System.Net;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // var ws = new WebService(8001);

            StorageService ss = new StorageService();

            QuestionsSet qs = ss.ParseTextFile(
                @"C:\Users\Krzysztof\Desktop\dane\Kod\Git\AON-PROJECT\Gui\AwanturaTests\TestsData\pytania.tsv",
                QuestionsSet.TextFormatParser);

            ss.SerializeToXMLFile(qs, 
                @"C:\Users\Krzysztof\Desktop\dane\Kod\Git\AON-PROJECT\Gui\AwanturaTests\TestsData\pytania.xml");

            Console.ReadKey();
        }
    }
}
