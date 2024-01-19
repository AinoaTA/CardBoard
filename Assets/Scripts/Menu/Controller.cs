using UnityEngine.SceneManagement;
using UnityEngine;

namespace Menu
{
    public class Controller : MonoBehaviour
    {
        /// <summary>
        /// Load gameplay scene.
        /// </summary>
        public void PlayGame() 
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}