using Microsoft.EntityFrameworkCore;
using StaffApi.Data.Interfaces;
using StaffApi.Entities;
using StaffApi.Interfaces;
using System.Linq.Expressions;

namespace StaffApi.Data.Repositories
{
    public class FamilyMemberRepository : IBaseRepository<FamilyMember>, IFamilyMemberRepository
    {
        private readonly AppDbContext context;

        public FamilyMemberRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<FamilyMember> AddAsync(FamilyMember entity)
        {
            await context.Set<FamilyMember>().AddAsync(entity);
            return entity;
        }

        public void Complete()
        {
            context.SaveChanges();
        }

        public void Delete(FamilyMember entity)
        {
            context.Set<FamilyMember>().Remove(entity);
        }

        //public IEnumerable<FamilyMember> FindAll(string[] includes = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<FamilyMember> FindAll(Expression<Func<FamilyMember, bool>> criteria, string[] includes = null)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<FamilyMember> FindFamiliesByStudent(int id)
        {
            return context.FamilyMembers.AsNoTracking().Where(a => a.studentId == id).Include(a=>a.student).ToList();
        }

        public async Task<FamilyMember> FindAsync(int? id)
        {
            return await context.FamilyMembers.AsNoTracking().Where(a => a.ID == id).FirstOrDefaultAsync();
        }

        public IEnumerable<FamilyMember> GetAll()
        {
            return context.FamilyMembers.AsNoTracking().ToList();
        }

        public FamilyMember Update(FamilyMember entity)
        {
            context.FamilyMembers.Update(entity);
            return entity;
        }

        public bool CheckStudent(int id)
        {
           var student= context.Students.Find(id);
            if (student != null) return true; 
            else return false;
        }
    }
}
