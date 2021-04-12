using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using AnotherTestProject;

namespace ConclaveAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Drag and Drop EE.log into a console then press Enter.");

            //Variables

            List<Player> players = new List<Player>();
            List<Map> maps = new List<Map>();
            List<string> usernames = new List<string>();
            List<string> mapNames = new List<string>();
            float sessionTime = 0;

            //Getting text from EE.log

            StreamReader stream = new StreamReader(Console.ReadLine());
            string log = stream.ReadToEnd();
            stream.Close();

            //Analyzing log

            string[] logText = log.Split(':');
            Console.WriteLine();

            //Finding Session Time

            sessionTime = float.Parse(logText[logText.Length - 4]
                .Substring(0, logText[logText.Length - 4]
                .IndexOf('S')-1)
                .Replace(" All smart pointers were destroyed!", ""));
            sessionTime = sessionTime / 3600.0f;

            //General analysis

            foreach (string line in logText)
            {
                if (line.Contains("- new avatar"))
                {
                    Player.GetPlayerFromLine(line, usernames, players);
                }

                Player.SetLocalPlayer(line);

                if (line.Contains("SetLevel")
                &&
                line.Contains("PVP"))
                {
                    Map.GetMapFromLine(line, mapNames, maps);
                }
            }
            //Counting Kills, Deaths, and K/D Ratio

            foreach (Player player in players)
            {
                foreach (string line in logText)
                {
                    if (line.Contains(player.username) && line.Contains("was killed by"))
                    {
                        Player.GetKillDeath(line, player);
                    }
                }
            }

            //Collecting stats for every map

            foreach (Map map in maps)
            {
                foreach (string line in mapNames)
                {
                    if (map.logName == line)
                    {
                        map.roundsPlayed++;
                    }
                }
                if (map.roundsPlayed > Map.maxRounds) { Map.maxRounds = map.roundsPlayed; }
            }

            //Output

            Console.Write("\t\t\t\t\t[Session stats]\n\n\t\tSession time: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write((float)Math.Round(sessionTime, 2)
                + "hrs");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(".\n\n\t\t\t\t\t[Players stats]\n");

            foreach (Player player in players)
            {
                player.WriteStats();
            }

            Console.WriteLine("\n\t\t\t\t\t\t[Maps]\n");

            foreach (Map map in maps)
            {
                map.WriteStats();
            }
            Console.ReadKey();
        }
    }
}
