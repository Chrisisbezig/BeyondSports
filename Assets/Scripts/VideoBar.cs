using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoBar : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider videoSlider;

    public void UpdateSlider()
    {
        ReplayManager.instance.CurrentFrameIndex = Mathf.RoundToInt(videoSlider.value * ReplayManager.instance.GetFrameCount());
    }

    private void Update()
    {
        videoSlider.value = (float)ReplayManager.instance.CurrentFrameIndex / ReplayManager.instance.GetFrameCount();
    }
}
