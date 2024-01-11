using UnityEngine;
using TMPro;
using Cardboard.Interactions;

namespace Cardboard.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _posInfo;
        [SerializeField] private TMP_Text _crono;

        #region enables
        private void OnEnable()
        {
            InteractableZone.OnUpdate += UpdateInfoText;
            Controller.OnTimer += UpdateCrono;
        }

        private void OnDisable()
        {
            InteractableZone.OnUpdate -= UpdateInfoText;
            Controller.OnTimer -= UpdateCrono;
        }
        #endregion

        private void Start()
        {
            _posInfo.ClearMesh();
        }

        private void UpdateInfoText(InteractableZone iz)
        {
            _posInfo.text = iz.NameZone;
        }

        private void UpdateCrono(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);

            _crono.text = "Tiempo restante: "+ string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }
}