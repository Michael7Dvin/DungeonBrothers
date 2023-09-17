﻿using Infrastructure.CodeBase.StateMachine;
using Infrastructure.CodeBase.StateMachine.Interfaces;
using Infrastructure.GameFSM;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Initialization
{
    public class ProjectContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            BindStateMachine(builder);
        }

        private void BindStateMachine(IContainerBuilder builder)
        {
            builder.Register<IStateMachine, StateMachine>(Lifetime.Singleton);
            builder.Register<BootstrapState>(Lifetime.Singleton);
        }
    }
}