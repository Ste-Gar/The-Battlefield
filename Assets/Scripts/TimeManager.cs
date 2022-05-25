using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public void SlowTime(float timescaleReduction)
    {
        Time.timeScale = timescaleReduction;
        Time.fixedDeltaTime = timescaleReduction * 0.02f;
    }

    public void ResetTimescale()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
}
