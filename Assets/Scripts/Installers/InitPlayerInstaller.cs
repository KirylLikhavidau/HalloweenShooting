using UnityEngine;
using Zenject;
using GameInput;

namespace Infrastructure
{
    public class InitPlayerInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _startPoint;


        public override void InstallBindings()
        {
            BindInstallerInterfaces();
        }

        private void BindInstallerInterfaces()
        {
            Container
                .BindInterfacesTo<InitPlayerInstaller>()
                .FromInstance(this);
        }

        public void Initialize()
        {
            GameObject player = Container.InstantiatePrefab(_playerPrefab, _startPoint.position, _startPoint.rotation, null);
        }
    }
}


