public class LoginResponse
{
    public string user_id { get; set; }
    public int tutorial {  get; set; }
    public LoginResponse()
    {
        user_id = "";
        tutorial = 0;
    }
}