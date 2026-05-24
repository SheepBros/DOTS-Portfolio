using Cysharp.Threading.Tasks;

namespace Portfolio
{
    public interface ILoadingView
    {
        UniTask Show();

        UniTask Hide();
    }
}