using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    //[SerializeField] string playerArmyLayer = "PlayerArmy";
    [SerializeField] string friendlyArmyTag = "Friendly";
    [SerializeField] string enemyArmyTag = "Enemy";
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
            //Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 worldPos = new Vector3(mousePos.x, 0, mousePos.z);
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity);
            if (!hasHit) return;
            if(hit.collider.CompareTag(enemyArmyTag) || hit.collider.CompareTag(friendlyArmyTag))
            {
                target = hit.collider.gameObject;
                target.GetComponent<SoldierController>().enabled = false;
                target.GetComponent<PlayerController>().enabled = true;
                //target.gameObject.layer = LayerMask.NameToLayer(playerArmyLayer);
                target.tag = friendlyArmyTag;

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
