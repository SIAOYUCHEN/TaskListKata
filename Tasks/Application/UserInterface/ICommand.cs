namespace Tasks.Application.UserInterface
{
    public interface ICommand
    {
        bool CanHandle(string command);
        
        void Execute(string[] arguments);
    }
}