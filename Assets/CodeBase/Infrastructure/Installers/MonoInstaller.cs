using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    public abstract class MonoInstaller : VContainer.Plagins.VContainer.Runtime.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            
        }

        public virtual void InstallBindings() { }
    }
}