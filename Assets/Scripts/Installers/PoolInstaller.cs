using Factory;
using Zenject;

namespace Infrastructure
{
    public class PoolInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPool();
        }

        private void BindPool()
        {
            Container
                .Bind<IPool<EnemyType>>()
                .To<EnemyPool<EnemyType>>()
                .AsSingle();
        }
    }
}


