using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilBase : MonoBehaviour
{
    //THIS IS SUPPOSED TO SERVE FOR ALL PROJECTILS
    //PROJECTILS IGNORE LAYERS, DEAL DAMAGE AND DECAY.

    Vector3 shootDir;
   [HideInInspector] public float damage;

    public void SetUp(Vector3 _shootDir, float _damage)
    {
        shootDir = _shootDir;
        damage = _damage;

        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        MoveProjectil();
    }

    void MoveProjectil()
    {
        transform.position += shootDir * 10 * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //WE DEAL DAMAGE IF WE CAN AND EXPLODE


        if(collision.collider.tag == "Player")
        {       
            return;
        }

        if(collision.collider.GetComponent<IDamageable>() != null)
        {

            //I PUSH THE FELLA A BIT BACK. AND PLAY THE DAMAGE ANIMATION.

            //Push(collision.collider.GetComponent<Rigidbody2D>());
            collision.collider.GetComponent<IDamageable>().ReceiveDamage(damage);
        }


        Destroy(gameObject);
    }

    void Push(Rigidbody2D rb)
    {

        //IT HAS ALWAYS TO BE OPOSITE OF THE DIRECTION THIS THING IS GOING.

        //ALSO PLAY ANIMATION AND GETS STUNNED FOR A SECOND.

        rb.AddForce(-shootDir, ForceMode2D.Force); //

    }


    public void TriggerProjectil(StateController controller)
    {
        controller.ReceiveEmptyDamage(damage);

        Destroy(gameObject);
    }
}
