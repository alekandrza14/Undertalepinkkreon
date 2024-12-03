using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform : MonoBehaviour
{

    public static List<Platform> platforms = new List<Platform>();
    public static bool init;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HyperbolicCamera>())
        {
            platforms.Add(this);
            init = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HyperbolicCamera>())
        {
            platforms.Remove(this);
        }
    }
}
