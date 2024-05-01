using Architecture.States;
using Architecture.States.Services.Interfaces;
using Zenject;

namespace Architecture.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInterfaces();
        }

        public void Initialize()
        {
            Container
                .Resolve<ICreateStatesFacade>()
                .CreateStates();
            
            Container
                .Resolve<IStateMachine>()
                .Enter<BootstrapState>();
        }

        private void BindInterfaces()
        {
            Container
                .BindInterfacesTo<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }
    }
}