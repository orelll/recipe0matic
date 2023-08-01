namespace Application;

public class IdGenerator:IIdGenerator
{
    public string Generate() => Guid.NewGuid().ToString("N");
}