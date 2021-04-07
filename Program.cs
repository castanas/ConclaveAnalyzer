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
            int maxRounds = 0;
            float sessionTime = 0;

            //Getting text from EE.log

            StreamReader stream = new StreamReader(Console.ReadLine());
            string log = stream.ReadToEnd();
            stream.Close();

            //Analyzing log

            string localPlayer = null;
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
                    if (!usernames.Contains(line.Replace(" - new avatar", "")) 
                        && 
                        line.Replace(" - new avatar", "") 
                        != " Player" )
                    {
                        usernames.Add(line.Replace(" - new avatar", ""));
                        players.Add(new Player(line.Replace(" - new avatar", "").Replace(" ","")));
                    }
                }

                if (line.Contains("Logged in"))
                {
                    localPlayer = line.Substring(11).Substring(0, line.Substring(11).IndexOf('(')).Replace(" ", "");
                }

                if (line.Contains("SetLevel") 
                    &&
                    line.Contains("PVP"))
                {
                    if (!mapNames.Contains(line.Substring(9)
                        .Remove(line.Substring(9)
                        .IndexOf('\n'))
                        .Replace("/Lotus/Levels/PVP/", "")
                        .Replace("\r", "")))
                    {
                        maps.Add(new Map(line.Substring(9)
                            .Remove(line.Substring(9)
                            .IndexOf('\n'))
                            .Replace("/Lotus/Levels/PVP/", "")
                            .Replace("\r", "")));
                    }
                    mapNames.Add(line.Substring(9)
                        .Remove(line.Substring(9).IndexOf('\n'))
                        .Replace("/Lotus/Levels/PVP/", "")
                        .Replace("\r",""));
                }
            }

            //Counting Kills, Deaths, and K/D Ratio

            foreach (Player player in players)
            {
                foreach (string line in logText)
                {
                    if (line.Contains(player.username) && line.Contains("was killed by"))
                    {
                        if (line.IndexOf(player.username) < line.IndexOf("was killed by"))
                        {
                            player.AddDeath();
                            player.CountKDRatio();
                        }
                        if (line.IndexOf(player.username) > line.IndexOf("was killed by"))
                        {
                            player.AddKill();
                            player.CountKDRatio();
                        }
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
                if (map.roundsPlayed > maxRounds) { maxRounds = map.roundsPlayed; }
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
                if (player.username == localPlayer)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(player.GetStats()+" (You)\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(player.GetStats()+"\n");
                }
            }
            Console.WriteLine("\n\t\t\t\t\t\t[Maps]\n");
            foreach (Map map in maps)
            {
                if (map.roundsPlayed == maxRounds)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(map.GetStats() + " (Most Played)\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(map.GetStats() + "\n");
                }
            }
            Console.ReadKey();
        }
    }
}
