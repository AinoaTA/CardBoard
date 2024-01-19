using UnityEngine;
using UnityEngine.InputSystem;

namespace Cardboard.Inputs
{
    public class InputManager : MonoBehaviour
    {
        public delegate void DelegateMove(Vector3 vector);
        public static DelegateMove OnMoveDelegate;

        public delegate void DelegateCameraMove(float value);
        public static DelegateCameraMove OnHorizontalMovement; 
        public static DelegateCameraMove OnVerticalMovement;

        public delegate void DelegateShot();
        public static DelegateShot OnShotDelegate;

        /// <summary>
        /// To get Axis values (x,y).
        /// </summary>
        /// <param name="ctx"></param>
        public void OnMove(InputAction.CallbackContext ctx)
        {
            OnMoveDelegate?.Invoke(ctx.ReadValue<Vector2>());
        }

        /// <summary>
        /// To get horizontal move value (X).
        /// </summary>
        /// <param name="ctx"></param>
        public void OnCameraHorizontalMove(InputAction.CallbackContext ctx)
        { 
            OnHorizontalMovement?.Invoke(ctx.ReadValue<float>());
        }

        /// <summary>
        /// To get vertical move value (Y).
        /// </summary>
        /// <param name="ctx"></param>
        public void OnCameraVerticalMove(InputAction.CallbackContext ctx)
        { 
            OnVerticalMovement?.Invoke(ctx.ReadValue<float>());
        }
    }
}