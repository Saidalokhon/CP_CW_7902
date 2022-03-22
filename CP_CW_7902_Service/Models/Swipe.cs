using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP_CW_7902_Service.Models
{
    public class Swipe
    {
        public Swipe(string swipe)
        {
            try
            {
                Id = swipe.Split(',')[0];
                SwipeTime = DateTime.ParseExact(swipe.Split(',')[1], "yyyy-MM-dd HH:mm:ss", null);
                Direction =
                    swipe.Split(',')[2].ToLower().Equals("in") ? Directions.IN :
                    swipe.Split(',')[2].ToLower().Equals("out") ? Directions.OUT :
                    throw new Exception("Wrong direction format. Direction should be either \"in\" or \"out\"");
            }
            catch (Exception ex)
            {
                throw new Exception($"Wrong swipe format: {ex.Message}");
            }
        }

        public string Id { get; set; }
        public DateTime SwipeTime { get; set; }
        public Directions Direction { get; set; }

        public enum Directions
        {
            IN,
            OUT
        }
    }
}