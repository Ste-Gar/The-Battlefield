using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] string friendlyArmyTag = "Friendly";
    [SerializeField] string enemyArmyTag = "Enemy";
    [SerializeField] [Range(0.01f, 1)] float timescaleReduction = 0.1f;

    [SerializeField] float pushBackRange = 3;
    [SerializeField] float pushBackPower = 10;
    [SerializeField] LayerMask layerMask;
    [SerializeField] ParticleSystem shockwavePrefab;

    TimeManager timeManager;
    GameObject target;
    bool isPossessing = false;

    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        timeManager = FindObjectOfType<TimeManager>();
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
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity);
            if (!hasHit) return;
            if(hit.collider.CompareTag(enemyArmyTag) || hit.collider.CompareTag(friendlyArmyTag))
            {
                target = hit.collider.gameObject;
                TakeControl(target);
                SwapTags(target);
                PushBack();

                isPossessing = true;

                timeManager.ResetTimescale();
            }
        }
    }

    private void TakeControl(GameObject target)
    {
        transform.position = target.transform.position;
        target.GetComponent<SoldierController>().enabled = false;
        target.GetComponent<PlayerController>().enabled = true;
    }

    private void SwapTags(GameObject target)
    {
        target.tag = friendlyArmyTag;
    }

    private void PushBack()
    {
        Instantiate(shockwavePrefab, transform);

        Collider[] targets = Physics.OverlapSphere(transform.position, pushBackRange, layerMask);

        foreach(Collider target in targets)
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();

            rb.AddExplosionForce(pushBackPower, transform.position, pushBackRange);
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
        timeManager.SlowTime(timescaleReduction);
    }
}
