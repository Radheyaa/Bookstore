namespace Bookstore.Services
{
    public interface IUserService
    {
        string getUserId();
        bool IsAuthenticated();
    }
}