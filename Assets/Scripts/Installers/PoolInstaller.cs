using Configs;
using Enemies;
using Pool;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private PoolConfig _poolConfig;

        public override void InstallBindings()
        {
            BindPoolConfig();
            BindPool();
        }

        private void BindPoolConfig()
        {
            Container
                .Bind<PoolConfig>()
                .FromInstance(_poolConfig);
        }

        private void BindPool()
        {
            Container
                .BindMemoryPool<Skeleton, SkeletonPool>()
                .FromComponentInNewPrefab(_poolConfig.SkeletonPrefab)
                .AsSingle();
        }
    }
}


