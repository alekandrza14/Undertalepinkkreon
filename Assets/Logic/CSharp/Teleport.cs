using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] string scene;
    public static string outscene;
    public static int hp = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HyperbolicCamera>())
        {
            if (hp == 0) 
            {
                outscene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(scene);
            }
            else
            {
                hp -= 1;
            }
        }
    }
}
