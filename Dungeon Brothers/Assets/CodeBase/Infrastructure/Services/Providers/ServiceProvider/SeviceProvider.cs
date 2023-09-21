using CodeBase.Common.Observables;
using CodeBase.Infrastructure.Services.Providers.LevelDataProvider;

namespace CodeBase.Infrastructure.Services.Providers.ServiceProvider
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Observable<ILevelDataProvider> _levelDataProvider = new();

        public IReadOnlyObservable<ILevelDataProvider> LevelDataProvider => _levelDataProvider;

        public void SetLevelDataProvider(ILevelDataProvider levelDataProvider) => _levelDataProvider.Value = levelDataProvider;
    }
}