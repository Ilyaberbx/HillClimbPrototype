using Infrastructure.Service.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Service.Factory
{
    public interface IGameFactory : IService
    {
        public IAssetsProviderService Assets { get; }
        public GameObject CreatePlayer(Vector3 at);
        public GameObject CreateLevel(Vector3 at);
        public GameObject CreateHud();
    }
}