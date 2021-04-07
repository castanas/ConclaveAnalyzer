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
    }
}
