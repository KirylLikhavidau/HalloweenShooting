using Enemies;
using Zenject;

namespace Pool
{
    public class SkeletonPool : MemoryPool<Skeleton>
    {
        protected override void OnSpawned(Skeleton item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Skeleton item)
        {
            item.gameObject.SetActive(false);
        }
    }
}