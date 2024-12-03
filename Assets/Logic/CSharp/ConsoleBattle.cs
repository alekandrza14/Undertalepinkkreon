using UnityEngine;
using UnityEngine.UI;

public class ConsoleBattle : MonoBehaviour
{
    public Text data;
    static public ConsoleBattle console;
    private void Start()
    {
        console = GetComponent<ConsoleBattle>();
        data = GetComponent<Text>();
    }
}
