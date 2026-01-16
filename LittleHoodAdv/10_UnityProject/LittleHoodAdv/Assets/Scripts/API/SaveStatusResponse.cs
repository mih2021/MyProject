public class SaveStatusResponse
{
    public int tutorial { get; set; }
    public int stage1 { get; set; }
    public int stage2 { get; set; }
    public int stage3 { get; set; }
    public SaveStatusResponse()
    {
        tutorial = 0;
        stage1 = 0;
        stage2 = 0;
        stage3 = 0;
    }
}