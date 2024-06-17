using Core.EdTech.Domain.DataAccess.Interfaces;
using Core.EdTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EdTech.Infrastructure.Services.Abstracts
{
  public interface ICourseManagementService
  {
    public Task<Guid> AddCourse(Course course);
    public Task UpdateCourse(Course course);
    public Task<Course> GetCourse(Guid Id);
    public Task RemoveCourse(Guid Id);
    public Task<List<Course>> GetCourses();
  }
}
