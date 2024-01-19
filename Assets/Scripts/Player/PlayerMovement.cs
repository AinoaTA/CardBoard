using UnityEngine;
using Cardboard.Inputs;

namespace Cardboard.Player
{
    public class PlayerMovement : MonoBehaviour, IBlocker
    {
        public bool Block { get => _block; set => _block = value; }
        private bool _block;

        [SerializeField] private float _speed;

        private CharacterController _character;
        private Vector3 _dir = Vector3.zero;
        private Camera _cam;

        private void OnEnable()
        {
            InputManager.OnMoveDelegate += Movement;
        }

        private void OnDisable()
        {
            InputManager.OnMoveDelegate -= Movement;
        }

        private void Start()
        {
            _cam = Camera.main;

            TryGetComponent(out _character);
        }

        private void Movement(Vector3 dir)
        {
            _dir = dir;
        }

        private void Update()
        { 
            float realSpeed = _speed * Time.deltaTime;

            Vector3 dir = _cam.transform.forward * _dir.y + _cam.transform.right * _dir.x;
            dir.y = 0;
            dir.Normalize();

            _character.transform.position += dir * realSpeed;

            //I was using .Move(dir) from character controller but there were some problemas about teleporting into 
            //interactable zones meanwhile player was walking. Some conflict between transform.position & .Move().
        }

        public IBlocker NoticeBlocker()
        {
            TryGetComponent(out IBlocker b);

            return b;
        }
    }
}