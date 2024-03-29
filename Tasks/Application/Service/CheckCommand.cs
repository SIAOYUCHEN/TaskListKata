using System;
using Tasks.Application.UserInterface;

namespace Tasks.Application.Service
{
    public class CheckCommand : ICommand
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IConsole _console;

        public CheckCommand(ITaskRepository taskRepository, IConsole console)
        {
            _taskRepository = taskRepository;
            _console = console;
        }

        public bool CanHandle(string command) => command.StartsWith("check");

        public void Execute(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                throw new ArgumentException("Task ID is required.");
            }

            var id = int.Parse(arguments[0]);
            var task = _taskRepository.FindById(id);
            if (task == null)
            {
                _console.WriteLine($"Could not find a task with an ID of {id}.");
                return;
            }

            task.Done = true;
        }
    }
}