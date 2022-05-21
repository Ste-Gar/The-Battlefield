using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
public class PlayerController : MonoBehaviour
{
    Mover mover;
    Camera cam;
    Attacker attacker;

    Vector2 mousePos;
    Vector2 movement;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        cam = Camera.main;
        attacker = GetComponent<Attacker>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            attacker.Attack();
        }
    }

    private void FixedUpdate()
    {
        mover.Move(movement);
        mover.LookAt(mousePos);
    }
}
