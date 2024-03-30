using Tasks.Application.UserInterface;

namespace Tasks.Application.Service
{
    public class AddProjectCommand : ICommand
    {
        private readonly ITaskRepository _taskRepository;

        public AddProjectCommand(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public bool CanHandle(string command) => command.StartsWith("add project");

        public void Execute(string[] arguments)
        {
            var projectName = arguments[1].Split(' ');
            
            _taskRepository.AddProject(projectName[1]);
        }
    }
}