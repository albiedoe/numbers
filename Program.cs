using Algo.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Diagnostics;
using System.Text;

namespace Algo
{
    public class Program
    {
        Config config;
        private static World world = new World();
        static void Main(string[] args)
        {
            PrintIntro();

            string entry="-1";
            //debug section
            Run run = new Run(){
                Principal = 500,
                MonthlyContribution  = 100,
                StartDate = new DateTime(1990, 1, 1)
            };

            World world = new World();
            Run[] runs = new Run[] {run};
            world.Process(runs);

            while(entry != "8"){
                PrintMenu();
                entry = Console.ReadLine();
                switch(entry){
                    case "1":
                        StartRun();
                    break;

                    default:
                        Console.WriteLine($"{entry} is not a valid menu option.");
                    break;
                }

            }
            Console.WriteLine("GoodBye!");
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            StreamReader reader = new StreamReader(new FileStream("AppConfig.xml", FileMode.Open));

            Config config = (Config)serializer.Deserialize(reader);
            
            reader.Dispose();
        }
        private static void PrintIntro()
        {
            Console.WriteLine(@"******* Welcome to the Numbers App. *******
We try to find trends in the stock market on continuous contribution plans and try and beat the s&p 500.
");
        }

        private static void StartRun(){
            decimal principal = RetrienceDecimal("principal");
            decimal monthly = RetrienceDecimal("Monthly Contribution");
            
            Run run = new Run(){
                Principal = principal,
                MonthlyContribution  = monthly
            };
            World world = new World();
            Run[] runs = new Run[] {run};
            world.Process(runs);

            Console.WriteLine(run.ToString());
            Console.WriteLine("");
        }

        private static int RetrieveInt(string fieldName){
            Console.WriteLine($"Please enter a value for {fieldName}.");
            string val = Console.ReadLine();
            int tempInt;
            while(!int.TryParse(val, out tempInt)){
                Console.WriteLine($"Unable to parse {val} to an int. Please enter a valid value.");
                val = Console.ReadLine();
            }
            return tempInt;
        }


        private static decimal RetrienceDecimal(string fieldName){
            Console.WriteLine($"Please enter a value for {fieldName}.");
            string val = Console.ReadLine();
            decimal tempDec;
            while(!decimal.TryParse(val, out tempDec)){
                Console.WriteLine($"Unable to parse {val} to an int. Please enter a valid value.");
                val = Console.ReadLine();
            }
            return tempDec;
        }

        private static void PrintMenu()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Hit a menu option to begin.");
            sb.AppendLine("1. Start a new Run.");
            sb.AppendLine("2. View Past Runs.");
            sb.AppendLine("8. Exit.");


            Console.WriteLine(sb.ToString());
        }
    }
}
