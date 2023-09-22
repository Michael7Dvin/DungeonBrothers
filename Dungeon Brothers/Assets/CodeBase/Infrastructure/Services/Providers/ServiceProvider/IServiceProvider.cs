using CodeBase.Common.Observables;
using CodeBase.Infrastructure.Services.Providers.LevelData;
using CodeBase.Infrastructure.Services.Providers.LevelDataProvider;

namespace CodeBase.Infrastructure.Services.Providers.ServiceProvider
{
    public interface IServiceProvider
    {
        public void SetLevelDataProvider(ILevelDataProvider levelDataProvider);
        
        public IReadOnlyObservable<ILevelDataProvider> LevelDataProvider { get; }

    }
}