using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 movement)
    {
        rb.AddForce(movement * moveSpeed, ForceMode2D.Force); //this has some drag and inertia. Feels better, but slower.

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    public void LookAt(Vector2 target)
    {
        Vector2 lookDir = target - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.SetRotation(angle);
        //rb.rotation = angle;

        //rb.SetRotation
    }
}
