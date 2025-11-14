using Enemies;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "Configs/PoolConfig")]
    public class PoolConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject PoolContainer {  get; private set; }
        [field: SerializeField] public Skeleton SkeletonPrefab { get; private set; }
    }
}