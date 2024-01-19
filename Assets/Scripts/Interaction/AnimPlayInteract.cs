using System.Collections;
using UnityEngine;

namespace Cardboard.Interactions
{
    public class AnimPlayInteract : Interactable
    {
        private Animator _animator;
        private float _timer = 1;
        private IEnumerator _routine;
         
        private void Awake()
        {
            TryGetComponent(out _animator);

            _animator.speed = 0;
        }
        public override void Interaction()
        {
            if (_routine != null) StopCoroutine(_routine);

            StartCoroutine(_routine = CounterRoutine());
        }

        private IEnumerator CounterRoutine()
        {
            float t = _timer;
            while (t >= 0)
            {
                t -= Time.deltaTime;

                _animator.speed = Mathf.Clamp(t, 0, _timer);
                yield return null;
            }
        }
    }
}