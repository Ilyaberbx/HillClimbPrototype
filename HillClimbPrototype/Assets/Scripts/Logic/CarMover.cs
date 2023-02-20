using System.Collections;
using Logic.UI;
using UnityEngine;

namespace Logic
{
    public class CarMover : MonoBehaviour, IPedalListener
    {
        [SerializeField] private Wheels _wheels;
        [SerializeField] private Rigidbody2D _carRigidbody;
        [SerializeField] private float _carTorque;
        private Pedal _gasPedal;
        private Pedal _brakePedal;

        private Coroutine _movingRoutine;
        private float _movement;
        private bool _isMoving;

        private void StartMoving(int movement)
        {
            if (_movingRoutine != null)
                StopMoving();

            _isMoving = true;
            _movement = movement;
            _movingRoutine = StartCoroutine(MoveRoutine());
        }

        private void StopMoving()
        {
            if (_movingRoutine == null) return;

            StopCoroutine(_movingRoutine);
            _movingRoutine = null;
            _movement = 0;
            _isMoving = false;
        }

        private IEnumerator MoveRoutine()
        {
            while (_isMoving)
            {
                yield return new WaitForFixedUpdate();
                Move();
                _wheels.Move(_movement);
            }
        }

        private void Move()
            => _carRigidbody.AddTorque(-_movement * _carTorque * Time.fixedDeltaTime);

        public void RegisterPedalCallbacks(Pedal pedal)
        {
            pedal.OnClickDown += StartMoving;
            pedal.OnClickUp += StopMoving;
        }
    }
}