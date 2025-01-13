using CodeBase.Infrastructure.ProjectContext;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.SceneContext
{
    public class SceneContext : VContainer.Plagins.VContainer.Runtime.Unity.LifetimeScope
    {
        protected override void Awake()
        {
            ProjectContextFactory.TryCreate();
            base.Awake();
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("SceneContext: Configure");
            RegisterLevelServices(builder);
            RegisterLevelStateMachine(builder);
        }

        protected virtual void RegisterLevelServices(IContainerBuilder builder)
        {
            
        }

        protected virtual void RegisterLevelStateMachine(IContainerBuilder builder)
        {
            
        }
        
    }
}

