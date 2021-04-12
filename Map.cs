using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherTestProject
{
    class Map
    {
        //Map variables
        public static int maxRounds = 0;
        public string map;
        public string logName;
        public int roundsPlayed = 0;
        public Map() { }

        //In this constructor program detects which map stands for presented logName
        public Map(string mapName)
        {
            logName = mapName;
            switch(mapName)
            {
                case "DMCorpusShipWarehouse.level": map = "Warehouse"; break;
                case "DMCorpusShipCoreRemaster.level": map = "Core (Remastered)"; break;
                case "DMGrnOcean.level": map = "Docking Bay"; break;
                case "DMShipyards.level": map = "Shipyards"; break;
                case "CTFGasCityRemaster.level": map = "Gas Works"; break;
                case "DMOroMoon.level": map = "Lua Ruins"; break;
                case "GrineerGalleonDuel01.level": map = "Navigation Array"; break;
                case "DMForestCompound.level": map = "Compound"; break;
                case "CTFOutpostCliffPort.level": map = "Outpost"; break;
                case "CTFOrokinMoonHalls.level": map = "Forgotten Halls"; break;
                case "GrineerSettlementDuel01.level": map = "Canyon Settlement"; break;
                case "DMCephalon.level": map = "Cephalon Citadel"; break;
                case "DMCrpCore.level": map = "Core"; break;
                case "DMFort.level": map = "Bunkers"; break;
                case "DMCorpusShip.level": map = "Freight Line"; break;
                case "CTFInfestedCorpus.level": map = "Infested Frigate"; break;
                default: map = "null"; break;
            }
        }


        //Returns map stats
        public string GetStats()
        {
            return map + ": " + roundsPlayed + " times";
        }

        //Gets map from a log line
        public static void GetMapFromLine(string line, List<string> mapNames, List<Map> maps)
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
                .Replace("\r", ""));
        }

        //Writes all map stats into console
        public void WriteStats()
        {
            if (roundsPlayed == maxRounds)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(GetStats() + " (Most Played)\n");
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
