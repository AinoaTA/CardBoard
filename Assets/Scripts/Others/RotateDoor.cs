using System.Collections;
using UnityEngine;

namespace Cardboard
{
    public class RotateDoor : MonoBehaviour
    {
        [SerializeField] private Vector3 _desiredAngle;

        private IEnumerator _routine;
        public void RotationDesired()
        {
            if (_routine != null) return;

            StartCoroutine(_routine = RotationRoutine());
        } 

        /// <summary>
        /// Lerping rotation.
        /// </summary>
        /// <returns></returns>
        IEnumerator RotationRoutine()
        {
            var init = transform.localRotation;
            float t = 0;
            float time = 1;
            while (t < time)
            {
                t += Time.deltaTime;
                transform.localRotation = Quaternion.Slerp(init, Quaternion.Euler(_desiredAngle), t / time);
                yield return null;
            }
        }
    }
}