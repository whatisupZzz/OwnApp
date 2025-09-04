
namespace Basket.API.Exception
{
    public class UserNotFoundExcpetion:NotFoundExpection
    {
        public UserNotFoundExcpetion(string userName) : base("未找到用户:", userName)
        {

        }
    }
}
