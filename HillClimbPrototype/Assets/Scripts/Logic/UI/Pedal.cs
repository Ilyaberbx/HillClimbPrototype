using System;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    public class Pedal : MonoBehaviour
    {
        public event Action OnClickUp;
        public event Action<int> OnClickDown;

        [Range(-1,1)]
        [SerializeField] private int _inputValue;

        [SerializeField] private Image _buttonImage;

        [SerializeField] private Sprite _clickedSprite;

        private Sprite _unClickedSprite;
        private bool _isClicked;

        private void Start() 
            => CacheStartSprite();

        private void OnMouseUp()
        {
            _buttonImage.sprite = _unClickedSprite;
            OnClickUp?.Invoke();
        }

        private void OnMouseDown()
        {
            _buttonImage.sprite = _clickedSprite;
            OnClickDown?.Invoke(_inputValue);
        }

        private void CacheStartSprite() 
            => _unClickedSprite = _buttonImage.sprite;
    }
}