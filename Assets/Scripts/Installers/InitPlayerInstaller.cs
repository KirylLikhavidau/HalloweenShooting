using UnityEngine;
using Zenject;
using GameInput;
using Zenject.SpaceFighter;

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

        private void BindPlayer(FPInputHandler instance)
        {
            Container
                .Bind<FPInputHandler>()
                .FromInstance(instance)
                .AsSingle();
        }

        public void Initialize()
        {
            FPInputHandler player = Container.InstantiatePrefabForComponent<FPInputHandler>(_playerPrefab, _startPoint.position, _startPoint.rotation, null);
            
            BindPlayer(player);
        }
    }
}


