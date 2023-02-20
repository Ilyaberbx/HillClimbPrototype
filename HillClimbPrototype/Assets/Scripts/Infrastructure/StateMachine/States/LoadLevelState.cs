using Infrastructure.Service.Factory;
using Infrastructure.Service.Scene;
using Infrastructure.StateMachine.States.StateInterfaces;
using Logic;
using Logic.UI;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerSpawnPointTag = "PlayerSpawnPoint";
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(LoadingCurtain loadingCurtain, ISceneLoaderService sceneLoader,IGameFactory gameFactory)
        {
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string payLoad)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(payLoad, OnLoaded);
        }

        public void Exit() { }

        private void OnLoaded()
        {
            InitGameWorld();
            _loadingCurtain.Hide();
        }

        private void InitGameWorld()
        {
            _gameFactory.CreateLevel(Vector3.zero);
            var player = _gameFactory.CreatePlayer(StartPoint());
            var hud = _gameFactory.CreateHud();
            InitPedalsInteraction(hud, player);
            InitCamera(player);
            InitHud(hud);
        }

        private void InitHud(GameObject hud) 
            => hud.GetComponent<Canvas>().worldCamera = Camera.main;

        private void InitCamera(GameObject player)
        {
            Camera viewCamera = Camera.main;
            if (viewCamera != null) 
                viewCamera.GetComponent<CameraFollow>().Construct(player.transform);
        }

        private void InitPedalsInteraction(GameObject hud, GameObject player) 
            => hud.GetComponent<PedalsActorUI>().Construct(player.GetComponent<IPedalListener>());

        private Vector3 StartPoint()
            => GameObject.FindGameObjectWithTag(PlayerSpawnPointTag).transform.position;
    }
}