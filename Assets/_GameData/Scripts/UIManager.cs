using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameData.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}
