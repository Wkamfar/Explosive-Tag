using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager 
{
    private Color tag;
    private Color flash;
    private Color notTag;
    private float FLASHING_TIME = 1;
    private float flashingTime;
    private float MIN_FLASHING_TIME = .5f;
    private bool isFlash;
    private float startTagTime;
    private float endTagTime;
    private bool didFlashingTimeStart;
    private TimeManager time;

    public ColorManager()
    {
        Start(new Color(255, 0, 0), new Color(255, 255, 255), new Color(0, 72, 255));
    }
    public ColorManager(Color _tag, Color _flash, Color _notTag)
    {
        Start(_tag, _flash, _notTag);
    }
    private void ConvertToMilliseconds()
    {
        FLASHING_TIME *= 1000;
        MIN_FLASHING_TIME *= 1000;
    }
    private void ResetTimer()
    {
        StartTimer(startTagTime, endTagTime);
    }
    public void StartTimer(float _startTagTime, float _endTagTime)
    {
        UpdateFlashingTime(_startTagTime, _endTagTime);
        startTagTime = _startTagTime;
        endTagTime = _endTagTime;
        didFlashingTimeStart = true;
        time.ResetEndTime(flashingTime);
    }
    private void UpdateFlashingTime(float _startTagTime, float _endTagTime)
    {
        float percentageTimeLeft = (_endTagTime - time.GetCurrentServerTime()) / (_endTagTime - _startTagTime);
        flashingTime = flashingTime < MIN_FLASHING_TIME ? MIN_FLASHING_TIME : FLASHING_TIME * percentageTimeLeft;
    }
    private void Start(Color _tag, Color _flash, Color _notTag)
    {
        tag = _tag;
        flash = _flash;
        notTag = _notTag;
        isFlash = false;
        didFlashingTimeStart = false;
        ConvertToMilliseconds();
        time = new TimeManager();
    }
    public bool IsFlashing()
    {
        return isFlash;
    }
    public void Update()
    {
        if (time.IsTimeUp())
        {
            isFlash = !isFlash;
            ResetTimer();
        }
    }
    public bool GetFlashingTimeStart()
    {
        return didFlashingTimeStart;
    }
    public void EndFlashing()
    {
        isFlash = false;
        didFlashingTimeStart = false;
    }
}
