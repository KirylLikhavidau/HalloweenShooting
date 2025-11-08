using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "Configs/PoolConfig")]
    public class PoolConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject PoolContainer {  get; private set; }
        [field: SerializeField] public int StartNumberOfObjects {  get; private set; }
    }
}