using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConclaveAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Drag and Drop EE.log into a console then press Enter.");

            //Lists

            List<Player> players = new List<Player>();
            List<string> usernames = new List<string>();

            //Getting text from EE.log

            StreamReader stream = new StreamReader(Console.ReadLine());
            string log = stream.ReadToEnd();
            stream.Close();

            //Finding all the playernames in EE.log, adding Players
            string localPlayer = null;
            string[] logText = log.Split(':');
            foreach (string line in logText)
            {
                if (line.Contains("- new avatar"))
                {
                    if (!usernames.Contains(line.Replace(" - new avatar", "")) && line.Replace(" - new avatar", "") != " Player" )
                    {
                        usernames.Add(line.Replace(" - new avatar", ""));
                        players.Add(new Player(line.Replace(" - new avatar", "").Replace(" ","")));
                    }
                }

                if (line.Contains("Logged in"))
                {
                    localPlayer = line.Substring(11).Substring(0, line.Substring(11).IndexOf('(')).Replace(" ", "");
                }
            }

            Console.WriteLine();

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

            //Output

            Console.WriteLine("Session stats: \n");
            foreach (Player player in players)
            {
                if (player.username == localPlayer)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(player.GetStats()+" (You)\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(player.GetStats()+"\n");
                }
            }
            Console.ReadKey();
        }
    }
}
