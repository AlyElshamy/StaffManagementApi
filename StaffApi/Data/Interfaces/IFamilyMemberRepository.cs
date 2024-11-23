using StaffApi.Entities;
using StaffApi.Interfaces;
using System.Linq.Expressions;

namespace StaffApi.Data.Interfaces
{
    public interface IFamilyMemberRepository:IBaseRepository<FamilyMember>
    {
        IEnumerable<FamilyMember> FindFamiliesByStudent(int id);
        bool CheckStudent(int id);
    }
}
