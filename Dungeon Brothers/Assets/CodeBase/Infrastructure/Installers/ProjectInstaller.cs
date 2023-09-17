using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            BindStateMachine(builder);
        }

        private void BindStateMachine(IContainerBuilder builder)
        {
            builder.Register<Bootstrapper>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<InitializationState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);
        }
    }
}