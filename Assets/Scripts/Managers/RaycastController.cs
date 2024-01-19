using UnityEngine;

namespace Cardboard
{
    public class RaycastController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _maxdistance = 200;
        [SerializeField] private LayerMask _layer;

        private Camera _cam;
        private Vector2 _middlePoint = new(0.5f, 0.5f);
         
        private Cardboard.Interactions.InteractableZone _currentZone;
        private float _maxLookTime = 1;
        private float _keepLookTime;

        public delegate void DelegateTimer(float time);
        public static DelegateTimer OnInteractionTimer;
         
        private void Awake()
        {
            _cam = Camera.main;
        }
        void Update()
        {
            Ray r = _cam.ViewportPointToRay(_middlePoint);
             
            if (Physics.Raycast(r, out RaycastHit hit, _maxdistance, _layer))
            { 
                if (_currentZone != null && _currentZone.gameObject == hit.collider.gameObject)
                    _keepLookTime = 0; //to avoid spamming and complete the visual help for player.

                else if (_keepLookTime >= _maxLookTime) //check if reaches the required time to do an interaction.
                {
                    _keepLookTime = 0;

                    hit.collider.TryGetComponent(out Cardboard.Interactions.Interactable i);

                    if (i is Cardboard.Interactions.InteractableZone) //just save interact if it is a zone.
                        _currentZone = i as Cardboard.Interactions.InteractableZone;
                      
                    i.Interaction(); //starts interaction
                }

                _keepLookTime += Time.deltaTime; 
            }
            else
            {
                _keepLookTime = 0;
            }

            //update UI or whatever subs.
            OnInteractionTimer?.Invoke(_keepLookTime / _maxLookTime);
        }
    }
}