namespace CQS_CoreService.Core.Vo;

public class AuthorizeLoginVo
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool UsePhone { get; set; } = false;
}

public class AuthorizeRegisterVo
{
    public string Username { get; set; }
    public string Password { get; set; }
}