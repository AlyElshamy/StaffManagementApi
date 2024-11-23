using Microsoft.EntityFrameworkCore;
using StaffApi.Data.Interfaces;
using StaffApi.Entities;
using StaffApi.Interfaces;
using StaffApi.Repositories;
using System.Linq.Expressions;

namespace StaffApi.Data.Repositories
{
    public class StudentRepository :  IStudentRepository
    {
        private readonly AppDbContext context;

        public StudentRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Student> AddAsync(Student entity)
        {
            await context.Set<Student>().AddAsync(entity);
            return entity;
        }

        public void Delete(Student entity)
        {
            context.Set<Student>().Remove(entity);
        }

        public async  Task<Student> FindAsync(int? id)
        {
            return await context.Students.AsNoTracking().Where(a=>a.ID==id).FirstOrDefaultAsync();

        }

        public IEnumerable<Student> GetAll()
        {
            return context.Students.ToList();
        }

      

        public Student Update(Student entity)
        {
            context.Students.Update(entity);
            return entity;
        }
        
        public Student SubmitStudent(Student entity)
        {
            entity.isSubmitted = true;
            context.Students.Update(entity);
               return entity;

        }
        public void Complete()
        {
            context.SaveChanges();
        }

    }
}
