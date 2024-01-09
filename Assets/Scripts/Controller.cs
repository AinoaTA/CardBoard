using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cardboard
{
    public class Controller : MonoBehaviour
    {
        public static Controller Instance; 
        public GameObject Player => _player;

        [Header("Scene References")]
        [SerializeField]private GameObject _player;

        private float _maxTimeInScene = 120;
        private float _currentTime = 0;

        private void OnEnable()
        {
            Interactions.InteractableZone.OnUpdate += UpdatePlayerPos;
        }

        private void OnDisable()
        {
            Interactions.InteractableZone.OnUpdate -= UpdatePlayerPos;
        }
         
        private void Awake()
        {
            Instance = this;
        }

        public void UpdatePlayerPos(Interactions.InteractableZone iz) 
        {
            _player.transform.position = iz.TeleportPoint.position;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _maxTimeInScene) 
            {
                _currentTime = 0;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}