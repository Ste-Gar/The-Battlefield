using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
public class PlayerController : MonoBehaviour
{
    Mover mover;
    Camera mainCam;
    Attacker attacker;

    Vector3 mousePos;
    Vector3 movement;

    public delegate void PossessedUnitDeath();
    public static event PossessedUnitDeath OnPossessedDeath;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        mainCam = Camera.main;
        attacker = GetComponent<Attacker>();
    }

    private void OnDisable()
    {
        OnPossessedDeath.Invoke();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0;
        movement.z = Input.GetAxisRaw("Vertical");

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;

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
