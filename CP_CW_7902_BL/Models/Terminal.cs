using CP_CW_7902_DAL.DBO;
using System;
using System.Collections.Generic;

namespace CP_CW_7902_BL.Models
{
    public class Terminal : ITerminal
    {
        public Terminal(Random random)
        {
            Ip = $"{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
            Status = Statuses.Waiting;
            Swipes = new List<Swipe>();
        }

        public void ParseSwipes(string swipesString)
        {
            foreach (string swipeString in swipesString.Split('\n')) Swipes.Add(new Swipe(swipeString));
        }

        public string Ip { get; set; }
        public Statuses Status { get; set; }
        public List<Swipe> Swipes { get; set; }
    }
}