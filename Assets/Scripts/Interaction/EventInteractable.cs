using UnityEngine.Events;
using UnityEngine;
using System.Collections;

namespace Cardboard.Interactions
{
    public class EventInteractable : Interactable
    {
        [SerializeField] private UnityEvent _interactEvent;
        [SerializeField] private float _maxCoolDown;

        private bool _coolDown;
        public override void Interaction()
        {
            if (_coolDown) return;
            _coolDown = true;

            _interactEvent?.Invoke();

            StartCoroutine(CooldownRoutine());
        }

        IEnumerator CooldownRoutine()
        {
            yield return new WaitForSeconds(_maxCoolDown);
            _coolDown = false;
        }
    }
}