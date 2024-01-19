using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cardboard
{
    public class Controller : MonoBehaviour
    {
        public static Controller Instance;
        public GameObject Player => _player;

        [Header("Scene References")]
        [SerializeField] private GameObject _player;

        private float _maxTimeInScene = 120;
        private float _currentTime = 0;

        //to update time of current scene.
        public delegate void DelegateTimer(float time);
        public static DelegateTimer OnTimer;

        //use for change state of blockers.
        public List<IBlocker> AllBlockers { get => _allBlockers; }
        private List<IBlocker> _allBlockers = new();

        private bool _currentStateBlocker = false;
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
        private void Update()
        {
            //Removing scene time.
            _currentTime -= Time.deltaTime;
            OnTimer?.Invoke(_currentTime);

            if (_currentTime <= 0) //then, go back to menu.
            {
                _currentTime = _maxTimeInScene;
                SceneManager.LoadScene("Menu");
            }
        }

        /// <summary>
        /// Change player position and keep Y.
        /// </summary>
        /// <param name="iz"></param>
        public void UpdatePlayerPos(Interactions.InteractableZone iz)
        { 
            //overriding Y pos.
            Vector3 newPos = iz.TeleportPoint.position;
            newPos.y = _player.transform.position.y;
             
            _player.transform.position = newPos;  
        } 
    }
}