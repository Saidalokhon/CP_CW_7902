using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_CW_7902_Service.Models
{
    public class Terminal
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
        public enum Statuses
        {
            Waiting,
            InProcess,
            Finished
        }
    }
}