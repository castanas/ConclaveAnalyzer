using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConclaveAnalyzer
{
    class Player
    {
        //Variables

        private static string localPlayer;
        public string username;
        public int kills;
        public int deaths;
        public float kdr;
        public float avgLifeTime; //Planned in future

        public Player() { }
        public Player(string name)
        {
            username = name;
        }

        //Increases kill count
        public void AddKill()
        {
            kills++;
        }

        //Increases death count
        public void AddDeath()
        {
            deaths++;
        }

        //Counts K/D ratio
        public void CountKDRatio()
        {
            kdr = (float)Math.Round((float)kills / (float)deaths, 2);
        }

        //Returns player stats
        public string GetStats()
        {
            return username +
                    " | Kills: " + kills +
                    " | Deaths: " + deaths +
                    " | K/D: " + kdr;
        }

        //Gets player from a log line
        public static void GetPlayerFromLine(string line, List<string> usernames, List<Player> players)
        {
            if (!usernames.Contains(line.Replace(" - new avatar", ""))
            &&
            line.Replace(" - new avatar", "")
            != " Player")
            {
                usernames.Add(line.Replace(" - new avatar", ""));
                players.Add(new Player(line.Replace(" - new avatar", "").Replace(" ", "")));
            }
        }

        //Sets localPlayer variable to log's owner
        public static void SetLocalPlayer(string line)
        {
            if (line.Contains("Logged in"))
            {
                localPlayer = line.Substring(11).Substring(0, line.Substring(11).IndexOf('(')).Replace(" ", "");
            }
        }

        //Gets info about what player got (Kill or Death)
        public static void GetKillDeath(string line, Player player)
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

        //Writes all player stats into console
        public void WriteStats()
        {
            if (username == localPlayer)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(GetStats() + " (You)\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(GetStats() + "\n");
            }
        }
    }
}
