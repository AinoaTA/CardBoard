using UnityEngine;

namespace Cardboard.Inputs
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _sensibilityX;
        [SerializeField] private float _sensibilityY;

        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _xClamp = 85f;

        private float _xRotation = 0;
        private float _mouseX, _mouseY;

        private void OnEnable()
        {
            InputManager.OnHorizontalMovement += HorizontalMovement;
            InputManager.OnVerticalMovement += VerticalMovement;
        }

        private void OnDisable()
        { 
            InputManager.OnHorizontalMovement -= HorizontalMovement;
            InputManager.OnVerticalMovement -= VerticalMovement;
        }

        ///
        private void Awake()
        {   
            //blocks and center the movement and visible of mouse.
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void HorizontalMovement(float inputMouse)
        { 
            _mouseX = inputMouse * _sensibilityX;
        }

        private void VerticalMovement(float inputMouse)
        { 
            _mouseY = inputMouse * _sensibilityY;
        }

        private void Update()
        { 
            transform.Rotate(Vector3.up, _mouseX * Time.deltaTime); //rotate player transform

            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);

            Vector3 targetRot = transform.eulerAngles;
            targetRot.x = _xRotation;

            _cameraTransform.eulerAngles = targetRot; //rotate camera transform
        }
    }
}