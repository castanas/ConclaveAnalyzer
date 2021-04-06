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

        public void AddKill()
        {
            kills++;
        }

        public void AddDeath()
        {
            deaths++;
        }

        public void CountKDRatio()
        {
            kdr = (float)Math.Round((float)kills / (float)deaths, 2);
        }

        public string GetStats()
        {
            return username +
                    " | Kills: " + kills +
                    " | Deaths: " + deaths +
                    " | K/D: " + kdr;
        }
    }
}
