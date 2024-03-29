using Tasks.Application.UserInterface;

namespace Tasks.Application.Service
{
    public class ErrorCommand : ICommand
    {
        private readonly IConsole _console;
    
        public ErrorCommand(IConsole console)
        {
            _console = console;
        }

        public bool CanHandle(string command) => command == "help";

        public void Execute(string[] arguments)
        {
            throw new System.NotImplementedException();
        }
    }
}