namespace CP_CW_7902_DAL.DBO
{
    public interface ITerminal
    {
        string Ip { get; set; }
        Statuses Status { get; set; }
    }
}
