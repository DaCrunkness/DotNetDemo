namespace Web.Configurations.Policies
{
    public interface INumberOfDaysForAccount
    {
        int Get(string userId);
    }
}