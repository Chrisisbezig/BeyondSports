using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSpeedSlider : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider speedSlider;

    public void UpdateSpeed()
    {
        switch (speedSlider.value)
        {
            case 0:
                ReplayManager.instance.replaySpeed = 0.1f;
                break;
            case 1:
                ReplayManager.instance.replaySpeed = 0.5f;
                break;
            case 2:
                ReplayManager.instance.replaySpeed = 1.0f;
                break;
            case 3:
                ReplayManager.instance.replaySpeed = 2.0f;
                break;
            case 4:
                ReplayManager.instance.replaySpeed = 5.0f;
                break;
        }
    }
}
