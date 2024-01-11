using UnityEngine;

namespace Cardboard
{
    public class RotateLooksPlayer : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position-(Controller.Instance.Player.transform.position + new Vector3(0,1,0)));
        }
    }
}