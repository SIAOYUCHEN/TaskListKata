using System.Collections.Generic;
using Tasks.Application.UserInterface;
using Tasks.Domain.Entity;

namespace Tasks.Application.Service
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void AddTask(string projectName, string taskDescription)
        {
            var task = new Task { Description = taskDescription, Done = false };
            _taskRepository.AddTask(projectName, task);
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _taskRepository.GetAllProjects();
        }

        public void AddProject(string projectName)
        {
            _taskRepository.AddProject(projectName);
        }
        
    }
}