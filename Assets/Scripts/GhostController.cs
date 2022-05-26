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

    [SerializeField] int maxEnergy = 10;
    [SerializeField] int possessionCost = 3;
    [SerializeField] int energyGain = 1;
    int currentEnergy;
    public int CurrentEnergy { get { return currentEnergy; } }

    TimeManager timeManager;
    GameObject target;
    bool isPossessing = false;

    Camera mainCam;

    public delegate void UpdateEnergy(int amount);
    public static UpdateEnergy OnUpdateEnergy;

    public delegate void InsufficientEnergy();
    public static InsufficientEnergy OnInsufficientEnergy;

    private void Awake()
    {
        mainCam = Camera.main;
        timeManager = FindObjectOfType<TimeManager>();

        currentEnergy = maxEnergy;
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
                RemoveEnergy();

                isPossessing = true;

                timeManager.ResetTimescale();

                GameManager.Instance.disableTutorial();
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

        if (currentEnergy < possessionCost)
        {
            OnInsufficientEnergy.Invoke();
            return;
        }
    }

    public void AddEnergy()
    {
        currentEnergy = Mathf.Min(currentEnergy += energyGain, maxEnergy);
        OnUpdateEnergy.Invoke(currentEnergy);
    }

    private void RemoveEnergy()
    {
        currentEnergy -= possessionCost;
        OnUpdateEnergy.Invoke(currentEnergy);
    }
}
