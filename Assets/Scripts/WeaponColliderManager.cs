using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderManager : MonoBehaviour
{
    [SerializeField] BoxCollider weaponCollider;

    #region Animation Events
    public void StartSwing()
    {
        weaponCollider.enabled = true;
    }

    public void EndSwing()
    {
        weaponCollider.enabled = false;
    }
    #endregion
}
