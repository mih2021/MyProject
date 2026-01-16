public class RankData
{
    public int rank {  get; set; }
    public string user_name { get; set; }
    public string clear_time { get; set; }

    public RankData()
    {
        rank = 0;
        user_name = string.Empty;
        clear_time = string.Empty;
    }
}
