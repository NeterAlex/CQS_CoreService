namespace CQS_CoreService.Application;

public interface ISystemService
{
    string GetDescription();
}

public class SystemService : ISystemService, ITransient
{
    public string GetDescription()
    {
        return "TEST";
    }
}