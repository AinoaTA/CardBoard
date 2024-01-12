using UnityEngine;
using TMPro;

namespace Cardboard.Interactions
{
    public class InteractableZone : Interactable
    {
        public Transform TeleportPoint => _teleportPoint;
        public string NameZone => _nameZone;

        [SerializeField] private Transform _teleportPoint;
        [SerializeField] private GameObject _lightReference;

        [SerializeField] private string _nameZone;

        public delegate void DelegateUpdate(InteractableZone zone);
        public static DelegateUpdate OnUpdate;

        private bool _using;
        public override void Interaction()
        {
            //avoid multiple entries if player is already in this point.
            if (_using) return;
            _using = true;
            _lightReference.SetActive(false);
           OnUpdate?.Invoke(this); 
        }

        public void ResetState()
        {
            _using = false; 
            _lightReference.SetActive(true);
        }
    }
}