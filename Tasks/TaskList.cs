using Tasks.Application.UserInterface;
using Tasks.Infrastructure;

namespace Tasks
{
	public sealed class TaskList
	{
		private const string Quit = "quit";
		private readonly IConsole _console;
		private readonly CommandFactory _commandFactory;

		public static void Main(string[] args)
		{
			var console = new RealConsole();
			var taskRepository = new TaskRepository();
			var commandFactory = new CommandFactory(taskRepository, console);

			new TaskList(console, commandFactory).Run();
		}

		public TaskList(IConsole console, CommandFactory commandFactory)
		{
			this._console = console;
			this._commandFactory = commandFactory;
		}

		public void Run()
		{
			while (true)
			{
				_console.Write("> ");
				var commandLine = _console.ReadLine();
				if (commandLine == Quit)
				{
					break;
				}

				var command = _commandFactory.GetCommand(commandLine);
				command.Execute(commandLine.Split(' ', 2));
			}
		}
	}
}
