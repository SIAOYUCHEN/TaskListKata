using System.Collections.Generic;
using Tasks.Domain.Entity;

namespace Tasks.Application.UserInterface
{
    public interface ITaskRepository
    {
        void AddTask(string projectName, Task task);
        
        Task FindById(long taskId);
        
        IEnumerable<Project> GetAllProjects();
        
        void AddProject(string projectName);
    }
}