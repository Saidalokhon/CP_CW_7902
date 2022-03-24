using System;

namespace CP_CW_7902_DAL.DBO
{
    public interface ISwipe
    {
        string SwipeId { get; set; }
        DateTime Time { get; set; }
        string Direction { get; set; }
    }
}
