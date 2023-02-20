using UnityEngine;

namespace Logic.UI
{
    public class PedalsActorUI : MonoBehaviour
    {
        [SerializeField] private Pedal _gasPedal;
        [SerializeField] private Pedal _brakePedal;

        public void Construct(IPedalListener listener)
        {
            listener.RegisterPedalCallbacks(_gasPedal);
            listener.RegisterPedalCallbacks(_brakePedal);
        }
    }
}