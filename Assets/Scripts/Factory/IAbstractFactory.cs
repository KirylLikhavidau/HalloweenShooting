using Enemies;
using UnityEngine;

namespace Factory
{
    public interface IAbstractFactory
    {
        void Create(EnemyType type, Vector3 at);
    }
}