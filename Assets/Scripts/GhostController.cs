using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    GameObject target;
    bool isPossessing = false;

    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        PlayerController.OnPossessedDeath += DetachFromUnit;
    }

    private void OnDisable()
    {
        PlayerController.OnPossessedDeath -= DetachFromUnit;
    }

    private void Update()
    {
        if (isPossessing) return;
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 worldPos = new Vector3(mousePos.x, mousePos.y, 0);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, worldPos);
            if (!hit) return;
            if(hit.collider.tag == "Fighter")
            {
                target = hit.collider.gameObject;
                target.GetComponent<EnemyController>().enabled = false;
                target.GetComponent<PlayerController>().enabled = true;

                isPossessing = true;
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;
        transform.position = target.transform.position;
    }

    private void DetachFromUnit()
    {
        isPossessing = false;
    }
}
