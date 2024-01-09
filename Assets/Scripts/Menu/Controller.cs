using UnityEngine.SceneManagement;
using UnityEngine;

namespace Menu
{
    public class Controller : MonoBehaviour
    {
        public void PlayGame() 
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}