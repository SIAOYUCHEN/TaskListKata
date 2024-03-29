using Tasks.Application.UserInterface;

namespace Tasks.Application.Service
{
    public class HelpCommand : ICommand
    {
        private readonly IConsole _console;
        
        public HelpCommand(IConsole console)
        {
            _console = console;
        }

        public bool CanHandle(string command) => command == "help";

        public void Execute(string[] arguments)
        {
            _console.WriteLine("Commands:");
            _console.WriteLine("  show");
            _console.WriteLine("  add project <project name>");
            _console.WriteLine("  add task <project name> <task description>");
            _console.WriteLine("  check <task ID>");
            _console.WriteLine("  uncheck <task ID>");
            _console.WriteLine();
        }
    }
}