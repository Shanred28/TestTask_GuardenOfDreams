using CodeBase.Entity.Character.Enemy;
using CodeBase.Infrastructure.Bootstraper;
using CodeBase.Services.Factory.EntityFactory;
using CodeBase.Services.Factory.EntityFactory.Interface;
using CodeBase.Services.StateMachine.LevelStateMachine;
using CodeBase.Services.StateMachine.LevelStateMachine.Interface;
using CodeBase.Services.StateMachine.LevelStateMachine.LevelState;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class LevelInstaller : SceneContext.SceneContext
    {
        [SerializeField] private SpawnZone spawnZone;
        protected override void RegisterLevelServices(IContainerBuilder builder)
        {
            Debug.Log("LEVEL: Install");
            builder.RegisterEntryPoint<LevelBootstrapper>();
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
        }
        
        protected override void RegisterLevelStateMachine(IContainerBuilder builder)
        {
            builder.Register<ILevelStateSwitcher, LevelStateMachine>(Lifetime.Singleton);

            builder.Register<LevelBootstrapState>(Lifetime.Singleton);
            builder.Register<LevelBootstrapMainMenuState>(Lifetime.Singleton);
            builder.Register<LevelBootstrapMainSceneState>(Lifetime.Singleton);
            builder.RegisterInstance(spawnZone).AsSelf();
        }
    }
}

