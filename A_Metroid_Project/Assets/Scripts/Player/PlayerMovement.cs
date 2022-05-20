using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //THIS CONTROLS TEH MOVEMENT.
    [SerializeField] int originalSpeed;
    [SerializeField] int normalSpeed;
    [SerializeField] int fastSpeed;

    [SerializeField] float jumpForce;
    int localDirection;

    #region BASIC MOVEMENT
    public void Move(int direction)
    {
        gameObject.transform.Translate(Vector3.right * direction * normalSpeed * Time.deltaTime);
        if (direction != 0) RotateSprite(direction);

        localDirection = direction;
    }
    void RotateSprite(int direction)
    {
        var playerVisual = transform.GetChild(0).gameObject;
        playerVisual.transform.localScale = new Vector3(-1 * direction, 1, 1);
    }
    #endregion

    public void Jump(float modifier)
    {
        Observer.instance.OnCameraJump(true, transform);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * jumpForce * modifier;

    }

}
