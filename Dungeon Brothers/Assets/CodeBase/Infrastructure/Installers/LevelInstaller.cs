using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.UI.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.Buttons;
using CodeBase.Infrastructure.Services.Factories.Characters;
using CodeBase.Infrastructure.Services.Factories.TileFactory;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.UI;
using CodeBase.Infrastructure.Services.Providers.LevelData;
using CodeBase.Infrastructure.Services.Providers.LevelDataProvider;
using CodeBase.Infrastructure.Services.Providers.ServiceProvider;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class LevelInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterFactories(builder);
        }

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<ITileFactory, TileFactory>(Lifetime.Singleton);
            builder.Register<ICommonUIFactory, CommonUIFactory>(Lifetime.Singleton);
            builder.Register<ITurnQueueViewFactory, TurnQueueViewFactory>(Lifetime.Singleton);
            builder.Register<ICharacterFactory, CharacterFactory>(Lifetime.Singleton);
            builder.Register<IButtonsFactory, ButtonsFactory>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<ITurnQueue, TurnQueue>(Lifetime.Singleton);
            builder.Register<IMapGenerator, MapGenerator>(Lifetime.Singleton);
            builder.Register<IMapService, MapService>(Lifetime.Singleton);

            builder.Register<LevelDataProvider>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}
