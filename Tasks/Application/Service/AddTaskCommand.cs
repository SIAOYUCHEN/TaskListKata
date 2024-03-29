using System;
using System.Linq;
using Tasks.Application.UserInterface;
using Tasks.Domain.Entity;

namespace Tasks.Application.Service
{
    public class AddTaskCommand : ICommand
    {
        private readonly ITaskRepository _taskRepository;

        public AddTaskCommand(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public bool CanHandle(string command) => command.StartsWith("add task");

        public void Execute(string[] arguments)
        {
            if (arguments.Length < 2)
            {
                throw new ArgumentException("Project name and task description are required.");
            }
            
            var projectName = arguments[0];
            var taskDescription = string.Join(" ", arguments.Skip(1));
            var task = new Task { Description = taskDescription, Done = false };
            _taskRepository.AddTask(projectName, task);
        }
    }
}