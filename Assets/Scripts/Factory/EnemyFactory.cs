using UnityEngine;
using Zenject;

namespace Factory
{
    public class EnemyFactory : IAbstractFactory
    {
        private IPool<EnemyType> _enemyPool;

        [Inject]
        private void Construct(IPool<EnemyType> pool)
        {
            _enemyPool = pool;
        }

        public void Load()
        {
            _enemyPool.Load();
        }

        public void Create(EnemyType type, Vector3 at)
        {
            switch (type)
            {
                case EnemyType.Skeleton:
                    GameObject obj = _enemyPool.GetObject(EnemyType.Skeleton);
                    obj.transform.position = at;
                    break;
            }
        }
    }
}
