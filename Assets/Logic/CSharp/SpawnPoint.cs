using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public HyperbolicCamera player;
    HyperbolicPoint self;
    public string outscene;
    private void Start()
    {
        self = GetComponent<HyperbolicPoint>();
        if (Teleport.outscene == outscene)
        {
            player.RealtimeTransform = self.HyperboilcPoistion.copy().inverse();
        }
    }

}
