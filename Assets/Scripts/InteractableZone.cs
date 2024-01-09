using UnityEngine;
using TMPro;

namespace Cardboard.Interactions
{
    public class InteractableZone : Interactable
    {
        public Transform TeleportPoint => _teleportPoint;
        public string NameZone => _nameZone;

        [SerializeField] private Transform _teleportPoint; 
        [SerializeField] private bool _using;

        [SerializeField] private TMP_Text _textZone;
        [SerializeField] private string _nameZone;

        public delegate void DelegateUpdate(InteractableZone zone);
        public static DelegateUpdate OnUpdate;

        private void Start()
        {
            _textZone.text = _nameZone; 
            _textZone.transform.parent.gameObject.SetActive(_using);
        }
        public override void Interaction()
        {
            //avoid multiple entries if player is already in this point.
            if (_using) return;
            _using = true;


            _textZone.transform.parent.gameObject.SetActive(_using);
            OnUpdate?.Invoke(this); 
        }

        public void ResetState()
        {
            _using = false;
            _textZone.transform.parent.gameObject.SetActive(_using);
        }
    }
}