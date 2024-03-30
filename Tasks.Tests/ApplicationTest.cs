using System.Collections.Generic;
using System.Linq;
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

		[Test]
		[TestCase(new string[] { "add project project1", "show", "quit" }, new string[] { "project1" })]
		[TestCase(new string[] { "add project project2", "add project project3", "show", "quit" }, new string[] { "project2", "project3" })]
		public void Should_Display_Projects_After_AddProject_And_Show_Commands(string[] commands, string[] expectedProjects)
		{
			var projectsDictionary = expectedProjects.ToDictionary(
				projectName => projectName, 
				projectName => new List<Task>() as IList<Task>
			);
			
			_taskRepository.GetAllProjects().Returns(projectsDictionary);
			
			_console.ReadLine().Returns(commands[0], commands.Skip(1).ToArray());
			
			_taskList.Run();
			
			_console.Received(commands.Length).Write("> ");
			
			foreach (var projectName in expectedProjects)
			{
				_console.Received(1).WriteLine(projectName);
			}
		}

		[Test]
		[TestCase(new string[] { "add project project1", "add task project1 test", "show", "quit" }, 
			new string[] { "project1" }, 
			new string[] { "test" })]
		[TestCase(new string[] { "add project project1", "add task project1 test", "add task project1 test2", "show", "quit" }, 
			new string[] { "project1" }, 
			new string[] { "test", "test2" })]
		public void Should_Display_Projects_After_AddProject_And_AddTask_And_Show_Commands(
			string[] commands, 
			string[] expectedProjects,
			string[] taskDescriptions)
		{
			var tasksForProject = taskDescriptions.Select((desc, index) => new Task { Id = index + 1, Description = desc, Done = false }).ToList();

			var projectsDictionary = expectedProjects.ToDictionary(
				projectName => projectName, 
				projectName => tasksForProject as IList<Task>
			);
    
			_taskRepository.GetAllProjects().Returns(projectsDictionary);
    
			_console.ReadLine().Returns(commands[0], commands.Skip(1).ToArray());
    
			_taskList.Run();
    
			_console.Received(commands.Length).Write("> ");
    
			foreach (var projectName in expectedProjects)
			{
				_console.Received(1).WriteLine(projectName);
				foreach (var task in tasksForProject)
				{
					_console.Received(1).WriteLine($"    [ ] {task.Id}: {task.Description}");
				}
			}
		}
	}
}
