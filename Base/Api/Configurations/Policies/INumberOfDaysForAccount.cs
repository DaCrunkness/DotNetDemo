namespace Api.Configurations.Policies
{
    public interface INumberOfDaysForAccount
    {
        int Get(string userId);
    }
}