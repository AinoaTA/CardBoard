using System.Linq;
using UnityEngine;

namespace Cardboard.Interactions
{
    public class InteractableZone : Interactable
    {
        public Transform TeleportPoint => _teleportPoint;
        public string NameZone => _nameZone;

        [SerializeField] private Transform _teleportPoint;
        [SerializeField] private GameObject _lightReference;
        [SerializeField] private string _nameZone;

        [Header("Interaction")]
        [SerializeField] private GameObject[] _lightsHelp; //the hint.

        public delegate void DelegateUpdate(InteractableZone zone);
        public static DelegateUpdate OnUpdate;

        public delegate void DelegatePreSettings(bool value);
        public static DelegatePreSettings OnPreSettings;

        private bool _using = false;
        private void Start()
        { 
            if (_lightsHelp.Length != 0)
                _lightsHelp.ToList().ForEach(n => n.SetActive(_using));
        }

        public override void Interaction()
        {
            //avoid multiple entries if player is already in this point.
            if (_using) return;

            _using = true;

            if (_lightsHelp.Length != 0)
                _lightsHelp.ToList().ForEach(n => n.SetActive(_using));

            _lightReference.SetActive(!_using);

            OnUpdate?.Invoke(this);
        }

        /// <summary>
        /// For reset the zone state.
        /// </summary>
        public void ResetState()
        {
            _using = false;
            _lightReference.SetActive(!_using);

            if (_lightsHelp.Length != 0)
                _lightsHelp.ToList().ForEach(n => n.SetActive(_using));
        }
    }
}