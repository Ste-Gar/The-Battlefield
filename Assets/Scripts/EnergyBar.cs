using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    HorizontalLayoutGroup group;
    Image[] bars;

    private void Awake()
    {
        group = GetComponent<HorizontalLayoutGroup>();
        bars = GetComponentsInChildren<Image>();
    }

    private void OnEnable()
    {
        GhostController.OnUpdateEnergy += UpdateEnergyBar;
    }

    private void OnDisable()
    {
        GhostController.OnUpdateEnergy -= UpdateEnergyBar;
    }

    private void UpdateEnergyBar(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            bars[i].enabled = true;
        }
        if (amount >= 10) return;

        for(int i = amount; i < 10; i++)
        {
            bars[i].enabled = false;
        }
    }
}
