namespace BBConfigurator.Worker
{
    public interface IBlackboxListner
    {
        event ActionOccurredHandler OnAction;
        void InitBlackbox();
        void TestAction(string command);
        void Restart();
    }
}