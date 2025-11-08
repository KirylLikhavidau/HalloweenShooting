using Factory;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private IAbstractFactory _enemyFactory;

        [Inject]
        private void Construct(IAbstractFactory factory)
        {
            _enemyFactory = factory;
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _enemyFactory.Load();
            _enemyFactory.Create(EnemyType.Skeleton, _spawnPoint.position);
        }
    }
}