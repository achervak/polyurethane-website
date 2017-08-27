namespace Polyurethane.Data.Interfaces.DataManagers
{
    public interface IConfigurationManager
    {
        string GetValue(string name, string defaultValue = "");
    }
}
