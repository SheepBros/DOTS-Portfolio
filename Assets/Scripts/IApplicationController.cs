namespace Portfolio
{
    public interface IApplicationController
    {
        bool IsQuitting { get; }
        
        void Quit();
    }
}