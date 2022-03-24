using CP_CW_7902_DAL.DBO;
using System;

namespace CP_CW_7902_BL.Models
{
    public class Swipe : ISwipe
    {
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