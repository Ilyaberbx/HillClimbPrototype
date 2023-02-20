using UnityEngine;

namespace Logic
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _target;
        [SerializeField] private Vector3 _offSet;

        public void Construct(Transform target)
        {
            _target = target;
            transform.position = CaculateStartPosition();
        }

        private Vector3 CaculateStartPosition() 
            => new Vector3(_target.position.x, _target.position.y,-10);
        
        private void Update() =>
            Follow();

        private void Follow()
        {
            if (_target == null) return;

            transform.position = _target.position + _offSet;
        }
    }
}