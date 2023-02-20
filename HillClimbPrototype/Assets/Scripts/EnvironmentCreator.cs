using UnityEngine;
using UnityEngine.U2D;

namespace Environment
{
    [ExecuteInEditMode]
    public class EnvironmentCreator : MonoBehaviour
    {
        [SerializeField] private int _cycles;
        [SerializeField] private float _perlinNoiseStep;
        [SerializeField] private float _xMultiplier;
        [SerializeField] private float _yMultiplier;
        [SerializeField] private float _xDifficulty;
        [SerializeField] private float _yDifficulty;
        [SerializeField] private float _curve;
        [SerializeField] private float _depth;
        private Vector3 _lastPosition;
        private SpriteShapeController _spriteShape;

        private void OnValidate()
        {
            _spriteShape = GetComponent<SpriteShapeController>();
            _spriteShape.spline.Clear();
            
            for (int i = 0; i < _cycles; i++)
            {
                _lastPosition = CalculateLastPosition(i);
                _spriteShape.spline.InsertPointAt(i,_lastPosition);

                if (i != 0 && i != _cycles - 1)
                {
                    UpdateSpriteShapeTangents(i);
                }

                _xMultiplier += _xDifficulty;
                _yMultiplier += _yDifficulty;
            }

            UpdateSpriteShapePoints();
        }

        private Vector3 CalculateLastPosition(int i) 
            => transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _perlinNoiseStep) * _yMultiplier);

        private void UpdateSpriteShapePoints()
        {
            _spriteShape.spline.InsertPointAt(_cycles, new Vector3(_lastPosition.x, transform.position.y - _depth));
            _spriteShape.spline.InsertPointAt(_cycles + 1, new Vector3(transform.position.x, transform.position.y - _depth));
        }

        private void UpdateSpriteShapeTangents(int i)
        {
            _spriteShape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            _spriteShape.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curve);
            _spriteShape.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curve);
        }
    }
}