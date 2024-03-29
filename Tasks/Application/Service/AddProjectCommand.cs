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
            var projectName = string.Join(" ", arguments);
            //var project = new Project { Name = projectName };
            _taskRepository.AddProject(projectName);
        }
    }
}