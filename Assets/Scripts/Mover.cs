using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotationSpeed = 10;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 movement)
    {
        rb.AddForce(movement * moveSpeed, ForceMode.Force); //this has some drag and inertia. not sure if it's better.
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    public void LookAt(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        lookDirection.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}
