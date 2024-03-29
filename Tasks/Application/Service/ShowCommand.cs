using Tasks.Application.UserInterface;

namespace Tasks.Application.Service
{
    public class ShowCommand : ICommand
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IConsole _console;
        
        public ShowCommand(ITaskRepository taskRepository, IConsole console)
        {
            _taskRepository = taskRepository;
            _console = console;
        }

        public bool CanHandle(string command) => command == "show";

        public void Execute(string[] arguments)
        {
            var projects = _taskRepository.GetAllProjects();
            foreach (var project in projects)
            {
                _console.WriteLine($"Project: {project.Name}");
                foreach (var task in project.Tasks)
                {
                    _console.WriteLine($"    [{(task.Done ? 'x' : ' ')}] {task.Id}: {task.Description}");
                }
            }
        }
    }
}