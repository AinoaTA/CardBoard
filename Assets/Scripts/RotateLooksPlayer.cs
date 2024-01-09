using UnityEngine;

namespace Cardboard
{
    public class RotateLooksPlayer : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Controller.Instance.Player.transform);
        }
    }
}