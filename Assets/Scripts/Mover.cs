using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 movement)
    {
        rb.AddForce(movement * moveSpeed, ForceMode.Force); //this has some drag and inertia. Feels better, but slower.

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    public void LookAt(Vector3 target)
    {
        //Vector3 lookDir = target - rb.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        //rb.SetRotation(angle);
        transform.LookAt(target);
    }
}
