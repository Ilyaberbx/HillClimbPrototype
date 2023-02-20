using Infrastructure.Service;
using Infrastructure.Service.AssetsProvider;
using Infrastructure.Service.Factory;
using Infrastructure.Service.Scene;
using Infrastructure.StateMachine.States.StateInterfaces;


namespace Infrastructure.StateMachine.States
{
    public class BootStrapState : IState
    {
        private const string Init = "Init";
        private const string PayLoad = "Gameplay";

        private readonly ISceneLoaderService _sceneLoader;
        private readonly ServiceLocator _serviceLocator;
        private readonly GameStateMachine _stateMachine;

        public BootStrapState(ISceneLoaderService sceneLoader, ServiceLocator serviceLocator,
            GameStateMachine stateMachine)
        {
            _sceneLoader = sceneLoader;
            _serviceLocator = serviceLocator;
            _stateMachine = stateMachine;
            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(Init, OnLoaded);

        public void Exit() {}

        private void OnLoaded()
            => _stateMachine.Enter<LoadLevelState, string>(PayLoad);

        private void RegisterServices()
        {
            RegisterSceneLoader();
            RegisterAssetProvider();
            RegisterFactory();
        }

        private void RegisterFactory()
            => _serviceLocator.RegisterService<IGameFactory>(
                new GameFactory(_serviceLocator.Single<IAssetsProviderService>()));

        private void RegisterAssetProvider()
            => _serviceLocator.RegisterService<IAssetsProviderService>(new AssetProvider());
        

        private void RegisterSceneLoader()
            => _serviceLocator.RegisterService<ISceneLoaderService>(_sceneLoader);
    }
}