using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Tasks.Application.UserInterface;
using Tasks.Domain.Entity;
using Tasks.Infrastructure;

namespace Tasks
{
	[TestFixture]
	public sealed class ApplicationTest
	{
		private IConsole _console;
		private CommandFactory _commandFactory;
		private TaskList _taskList;
		private ITaskRepository _taskRepository;

		[SetUp]
		public void SetUp()
		{
			_console = Substitute.For<IConsole>();
			_taskRepository = Substitute.For<ITaskRepository>();
			_commandFactory = new CommandFactory(_taskRepository, _console);
			_taskList = new TaskList(_console, _commandFactory);
		}

		[Test]
		public void Run_Quits_WhenQuitCommandIsEntered()
		{
			_console.ReadLine().Returns("quit");

			_taskList.Run();

			_console.Received(1).Write("> ");
		}

		
	}
}
