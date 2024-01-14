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
            //Interactions.InteractableZone.OnPreSettings += ChangeBlockersState;
            //IBlocker.OnBlocker += AddBlockerObject;
        }

        private void OnDisable()
        {
            Interactions.InteractableZone.OnUpdate -= UpdatePlayerPos;
            //IBlocker.OnBlocker -= AddBlockerObject;
            //Interactions.InteractableZone.OnPreSettings -= ChangeBlockersState;
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
            _currentTime -= Time.deltaTime;
            OnTimer?.Invoke(_currentTime);

            if (_currentTime <= 0)
            {
                _currentTime = _maxTimeInScene;
                SceneManager.LoadScene("Menu");
            }
        }

        public void UpdatePlayerPos(Interactions.InteractableZone iz)
        {
            Debug.Log("updating...");

            //overriding Y pos.
            Vector3 newPos = iz.TeleportPoint.position;
            newPos.y = _player.transform.position.y;
             
            _player.transform.position = newPos; 
            //ChangeBlockersState(false);
        }

        //private void AddBlockerObject(IBlocker blocker)
        //{
        //    if (blocker != null)
        //    {
        //        _allBlockers.Add(blocker); 
        //    }
        //}

        //public void ChangeBlockersState(bool toLock)
        //{
        //    if (_currentStateBlocker == toLock) return; //to avoid multiple entries and useless iterations (!)
        //    _currentStateBlocker = toLock;

        //    if (_allBlockers.Count != 0)
        //        _allBlockers.ForEach(n => n.Block = toLock);
        //}
    }
}