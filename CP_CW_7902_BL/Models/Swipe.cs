using CP_CW_7902_DAL.DBO;
using System;

namespace CP_CW_7902_BL.Models
{
    public class Swipe : ISwipe
    {
        /// <summary>
        /// The constructor creates the swipe from the passed string.
        /// </summary>
        /// <param name="swipe">The splitted string with swipe data taken from DLL function</param>
        public Swipe(string swipe)
        {
            SwipeId = swipe.Split(',')[0];
            Time = DateTime.ParseExact(swipe.Split(',')[1], "yyyy-MM-dd HH:mm:ss", null);
            Direction = swipe.Split(',')[2];
        }

        public string SwipeId { get; set; }
        public DateTime Time { get; set; }
        public string Direction { get; set; }
    }
}