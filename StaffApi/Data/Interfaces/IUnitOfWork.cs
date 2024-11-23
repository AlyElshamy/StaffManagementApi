using StaffApi.Entities;

namespace StaffApi.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<Student> Students { get; }
        IBaseRepository<FamilyMember> FamilyMembers { get; }
        void Complete();

    }
}
