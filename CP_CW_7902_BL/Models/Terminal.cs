using CP_CW_7902_DAL.DBO;
using System;
using System.Collections.Generic;

namespace CP_CW_7902_BL.Models
{
    public class Terminal : ITerminal
    {
        /// <summary>
        /// The constructor creates unique IP address for the termianl,
        /// sets its status to Waiting and creates new list of swipe objects.
        /// </summary>
        /// <param name="random">The Random class</param>
        public Terminal(Random random)
        {
            Ip = $"{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
            Status = Statuses.Waiting;
            Swipes = new List<Swipe>();
        }

        /// <summary>
        /// The method adds swipes to the swipe list.
        /// </summary>
        /// <param name="swipesString">The string with swipes data taken from DLL function </param>
        public void ParseSwipes(string swipesString)
        {
            foreach (string swipeString in swipesString.Split('\n')) Swipes.Add(new Swipe(swipeString));
        }

        public string Ip { get; set; }
        public Statuses Status { get; set; }
        public List<Swipe> Swipes { get; set; }
    }
}