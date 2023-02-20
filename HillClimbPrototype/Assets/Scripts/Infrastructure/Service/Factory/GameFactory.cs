using Data;
using Infrastructure.Service.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Service.Factory
{
    public class GameFactory : IGameFactory
    {
        public IAssetsProviderService Assets { get; }

        public GameFactory(IAssetsProviderService assets) 
            => Assets = assets;

        public GameObject CreatePlayer(Vector3 at)
        {
            var player = Assets.Instantiate(AssetsPath.Player);
            player.transform.position = at;
            return player;
        }

        public GameObject CreateLevel(Vector3 at)
        {
            var level = Assets.Instantiate(AssetsPath.Level);
            level.transform.position = at;
            return level;
        }

        public GameObject CreateHud()
            => Assets.Instantiate(AssetsPath.Hud);
    }
}