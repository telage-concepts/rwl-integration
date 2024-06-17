using Core.EdTech.Domain.DataAccess.Interfaces;
using Core.EdTech.Domain.Entities;
using Core.EdTech.Infrastructure.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EdTech.Infrastructure.Services.Implementations
{
  public class CourseManagementService : ICourseManagementService
  {
    private readonly IUnitOfWork<Course> unitOfWork;

    public CourseManagementService(IUnitOfWork<Course> unitOfWork)
    {
      this.unitOfWork = unitOfWork;
    }

    public async Task<Guid> AddCourse(Course course)
    {
      await unitOfWork.Repository.Insert(course);
      await unitOfWork.SaveAsync();
      return course.Id;
    }

    public async Task<Course> GetCourse(Guid Id)
    {
      return unitOfWork.Repository.Get(x => x.Id == Id).FirstOrDefault();
    }

    public async  Task<List<Course>> GetCourses()
    {
      return unitOfWork.Repository.Get().ToList();
    }

    public async  Task RemoveCourse(Guid Id)
    {
      unitOfWork.Repository.Delete(unitOfWork.Repository.Get(x => x.Id == Id).FirstOrDefault());
    }

    public async Task UpdateCourse(Course course)
    {
      unitOfWork.Repository.Update(course);
      await unitOfWork.SaveAsync();
    }


  }
}
