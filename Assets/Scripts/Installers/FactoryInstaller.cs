using Factory;
using Zenject;

namespace Infrastructure
{
    public class FactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactory();
        }

        private void BindFactory()
        {
            Container
                .Bind<IAbstractFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }
    }
}


