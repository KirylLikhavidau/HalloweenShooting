using UnityEngine;

namespace Factory
{
    public interface IAbstractFactory
    {
        void Load();
        void Create(EnemyType type, Vector3 at);
    }
}