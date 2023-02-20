using UnityEngine;

namespace Logic
{
    public class Wheels : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _backTireRigidbody;
        [SerializeField] private Rigidbody2D _frontTireRigidbody;
        [SerializeField] private float _speed;

        public void Move(float movement)
        {
            _backTireRigidbody.AddTorque(-movement * _speed * Time.fixedDeltaTime);
            _frontTireRigidbody.AddTorque(-movement * _speed * Time.fixedDeltaTime);
        }
    }
}