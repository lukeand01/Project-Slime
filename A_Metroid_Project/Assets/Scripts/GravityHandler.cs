using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHandler : MonoBehaviour
{
    public void ControlGravity(float amount)
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = amount;
    }
}
