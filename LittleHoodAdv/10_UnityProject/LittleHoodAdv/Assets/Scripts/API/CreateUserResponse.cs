using UnityEngine;

public class CreateUserResponse
{
    public bool success { get; set; }
    public string message { get; set; }
    public CreateUserResponse()
    {
        success = true;
        message = string.Empty;
    }
}
