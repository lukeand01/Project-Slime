using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilSentry : MonoBehaviour
{
    ///IT JUST DECAYS AFTER SOME TIME.
    ///

    float damage;

    public void SetUp(float _damage, float _timer)
    {
        damage = _damage;
        Invoke("Decay", _timer);
    }
    void Decay(float timer)
    {
        Destroy(gameObject);
    }

  public float GetDamageAndDestroy()
    {
        Destroy(gameObject);
        return damage;

    }


}
