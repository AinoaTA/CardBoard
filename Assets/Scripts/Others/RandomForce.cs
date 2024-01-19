using UnityEngine;

namespace Cardboard
{
    public class RandomForce : MonoBehaviour
    {
        private Rigidbody _rb;

        private void Awake()
        {
            TryGetComponent(out _rb);
        }

        /// <summary>
        /// Applying a random direction to a given force.
        /// </summary>
        public void ApplyForce()
        {
            Vector3 rnd = new(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));

            Debug.Log(rnd);
            _rb.AddForce(rnd * 5, ForceMode.Impulse);
        }
    }
}