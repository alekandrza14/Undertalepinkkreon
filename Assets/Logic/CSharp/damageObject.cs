using System;
using UnityEngine;

public class damageObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HyperbolicCamera>())
        {
            FightManager.instance.grabDamage();
            Destroy(gameObject);
        }
    }
}
