using UnityEngine;
[ExecuteAlways]
public class HyperbolicAnimationBone : MonoBehaviour
{
    HyperbolicPoint pos;
    public float time;
    public float m;
    public float s;
    public float n;
    void LateUpdate()
    {
        pos = this.GetComponent<HyperbolicPoint>();
        pos.HyperboilcPoistion.m = m;
        pos.HyperboilcPoistion.s = s;
        pos.HyperboilcPoistion.n = n;
    }
}
