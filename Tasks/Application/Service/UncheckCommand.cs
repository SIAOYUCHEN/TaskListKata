using System;
using Tasks.Application.UserInterface;

namespace Tasks.Application.Service
{
    public class UncheckCommand : ICommand
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IConsole _console;
        
        public UncheckCommand(ITaskRepository taskRepository, IConsole console)
        {
            _taskRepository = taskRepository;
            _console = console;
        }

        public bool CanHandle(string command) => command.StartsWith("uncheck");

        public void Execute(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                throw new ArgumentException("Task ID is required.");
            }

            int id;
            try
            {
                id = int.Parse(arguments[0]);
            }
            catch (FormatException)
            {
                _console.WriteLine("The task ID must be a number.");
                return;
            }
            
            var task = _taskRepository.FindById(id);
            if (task == null)
            {
                _console.WriteLine($"Could not find a task with an ID of {id}.");
                return;
            }

            task.Done = false;
        }
    }
}