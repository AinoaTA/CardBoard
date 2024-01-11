using UnityEngine;
using Cardboard.Interactions;
using System.Linq;

namespace Cardboard.Zone
{
    public class ZoneController : MonoBehaviour
    {
        [SerializeField] private InteractableZone[] _allZones;

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
            _allZones[Random.Range(0, _allZones.Length)].Interaction();
        }

        private void DoReset(InteractableZone z)
        {
            _allZones.ToList().ForEach(n =>
            {
                if (z.GetInstanceID() != n.GetInstanceID()) n.ResetState();
            });
        } 
    }
}