
namespace StaffApi.Buisness.UserCheck
{
    public interface IUserRoleCheck
    {
        Task<bool> checkrole(string userID);
        Task<bool> checkUser(string userID);
        bool checkRoleName(string RoleName);

    }
}