using Configs;
using Enemies;
using Pool;
using UnityEngine;
using Zenject;

namespace Factory
{
    public class EnemyFactory : IAbstractFactory, IInitializable
    {
        private SkeletonPool _skeletonPool;
        private GameObject _enemyContainer;

        private EnemyFactory(SkeletonPool pool, PoolConfig poolConfig)
        {
            _skeletonPool = pool;
            _enemyContainer = poolConfig.PoolContainer;
        }

        public void Initialize()
        {
            _enemyContainer = GameObject.Instantiate(_enemyContainer);
        }

        public void Create(EnemyType type, Vector3 at)
        {
            switch (type)
            {
                case EnemyType.Skeleton:
                    Skeleton skeleton = _skeletonPool.Spawn();
                    skeleton.transform.position = at;
                    skeleton.transform.SetParent(_enemyContainer.transform, true);
                    skeleton.Init();
                    break;
            }
        }

    }
}
