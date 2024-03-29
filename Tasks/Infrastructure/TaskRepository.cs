using System.Collections.Generic;
using System.Linq;
using Tasks.Application.UserInterface;
using Tasks.Domain.Entity;

namespace Tasks.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDictionary<string, Project> _projects = new Dictionary<string, Project>();
        private long _lastId = 0;

        public void AddTask(string projectName, Task task)
        {
            if (!_projects.TryGetValue(projectName, out var project))
            {
                project = new Project { Name = projectName };
                _projects[projectName] = project;
            }
            task.Id = ++_lastId;
            project.Tasks.Add(task);
        }

        public Task FindById(long taskId)
        {
            return _projects
                .SelectMany(p => p.Value.Tasks)
                .FirstOrDefault(t => t.Id == taskId);
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _projects.Values;
        }

        public void AddProject(string projectName)
        {
            if (!_projects.ContainsKey(projectName))
            {
                _projects[projectName] = new Project { Name = projectName };
            }
        }
    }
}