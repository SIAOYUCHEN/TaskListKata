using Tasks.Application.UserInterface;

namespace Tasks.Application.Service
{
    public class UnknownCommand : ICommand
    {
        private readonly IConsole _console;
        private readonly string _commandText;

        public UnknownCommand(IConsole console, string commandText)
        {
            _console = console;
            _commandText = commandText;
        }

        public bool CanHandle(string command) => true;

        public void Execute(string[] arguments)
        {
            _console.WriteLine($"I don't know what the command \"{_commandText}\" is.");
        }
    }
}