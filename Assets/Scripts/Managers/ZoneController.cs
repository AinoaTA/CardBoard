using UnityEngine;
using Cardboard.Interactions;
using System.Linq;

namespace Cardboard.Zone
{
    public class ZoneController : MonoBehaviour
    {
        /*[SerializeField]*/
        private InteractableZone[] _allZones;
        [SerializeField] private bool _setRandomZone = true;
        private void OnEnable()
        {
            InteractableZone.OnUpdate += DoReset;
        }

        private void OnDisable()
        {
            InteractableZone.OnUpdate -= DoReset;
        }

        private void Start()
        {
            //to avoid any missed zone. Not use frequently.
            _allZones = GameObject.FindObjectsOfType<InteractableZone>();

            if (_setRandomZone)
                _allZones[Random.Range(0, _allZones.Length)].Interaction();
        }

        /// <summary>
        /// Reset all zones except current plauce. 
        /// </summary>
        /// <param name="z"></param>
        private void DoReset(InteractableZone z)
        {
            _allZones.ToList().ForEach(n =>
            {
                if (z.GetInstanceID() != n.GetInstanceID()) n.ResetState();
            });
        }
    }
}