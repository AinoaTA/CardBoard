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

        public delegate void DelegateTimer(float time);
        public static DelegateTimer OnTimer;

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

        private void Start()
        {
            _currentTime = _maxTimeInScene;
        }

        public void UpdatePlayerPos(Interactions.InteractableZone iz) 
        {
            //to avoid override Y pos.
            Vector3 newPos = iz.TeleportPoint.position;
            newPos.y = _player.transform.position.y;

            _player.transform.position = newPos;
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;
            OnTimer?.Invoke(_currentTime);

            if (_currentTime <= 0) 
            {
                _currentTime = _maxTimeInScene;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}