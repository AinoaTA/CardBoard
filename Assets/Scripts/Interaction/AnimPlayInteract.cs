using System.Collections; 
using UnityEngine;

namespace Cardboard.Interactions
{
    public class AnimPlayInteract : Interactable
    {
        private Animator _animator;
        private float _timer;
        private IEnumerator _routine;

        private void Awake()
        {
            TryGetComponent(out _animator);
        }
        public override void Interaction()
        {
            _timer = 1;
            if (_routine != null) StopCoroutine(_routine);

            StartCoroutine(_routine = CounterRoutine());
        }

        private IEnumerator CounterRoutine()
        {
            float t = _timer;
            while (t >= _timer)
            {
                t -= Time.deltaTime;
                _animator.speed = t;
                yield return null;
            }
        }
    }
}