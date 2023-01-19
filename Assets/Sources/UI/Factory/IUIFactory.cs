using Sources.Infrastructure.Services;

namespace Sources.UI.Factory
{
    public interface IUIFactory : IService
    {
        void CreateHud();
    }
}