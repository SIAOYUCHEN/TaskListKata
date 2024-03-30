using System.Collections.Generic;
using System.Linq;
using Tasks.Application.UserInterface;
using Tasks.Domain.Entity;

namespace Tasks.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDictionary<string, IList<Task>> _tasks = new Dictionary<string, IList<Task>>();
        private long _lastId = 0;

        public void AddTask(string projectName, Task task)
        {
            if (!_tasks.TryGetValue(projectName, out var projectTasks))
            {
                projectTasks = new List<Task>();
                _tasks[projectName] = projectTasks;
            }
            task.Id = ++_lastId;
            projectTasks.Add(task);
        }

        public Task FindById(long taskId)
        {
            return _tasks
                .SelectMany(kv => kv.Value)
                .FirstOrDefault(t => t.Id == taskId);
        }
        
        public IDictionary<string, IList<Task>> GetAllProjects()
        {
            return _tasks;
        }

        public void AddProject(string projectName)
        {
            if (!_tasks.ContainsKey(projectName))
            {
                _tasks[projectName] = new List<Task>();
            }
        }
    }
}