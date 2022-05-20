using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilEmpty : ProjectilBase
{


    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.gameObject.tag == "Player")
        {
            return;
        }

        Debug.Log("hit something");

        if (collision.transform.parent.gameObject.GetComponent<IDamageable>() != null)
        {
            Debug.Log("should have damaged");
            collision.transform.parent.gameObject.GetComponent<IDamageable>().ReceiveDamage(damage);
        }


        Destroy(gameObject);

    }
}
