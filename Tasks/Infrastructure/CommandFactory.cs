using System.Collections.Generic;
using System.Linq;
using Tasks.Application.Service;
using Tasks.Application.UserInterface;

namespace Tasks.Infrastructure
{
    public class CommandFactory
    {
        private readonly List<ICommand> _commands;
        private readonly IConsole _console;
        
        public CommandFactory(ITaskRepository taskRepository, IConsole console)
        {
            _console = console;
            _commands = new List<ICommand>
            {
                new ShowCommand(taskRepository, console),
                new AddProjectCommand(taskRepository),
                new AddTaskCommand(taskRepository),
                new CheckCommand(taskRepository, console),
                new UncheckCommand(taskRepository, console),
                new HelpCommand(console),
            };
        }

        public ICommand GetCommand(string commandLine)
        {
            var parts = commandLine.Split(' ', 2);
            var commandName = parts[0];
            var subCommand = parts.Length > 1 ? parts[1] : string.Empty;

            var command = _commands.FirstOrDefault(c => c.CanHandle($"{commandName} {subCommand}".Trim()));

            if (command != null)
            {
                return command;
            }

            return new UnknownCommand(_console, commandLine);
        }
    }
}