using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TimeManager 
{
    private float currentTime;
    private float endTime;
    private const int SECONDS_TO_MS = 1000;
    //Time in milliseconds 1000ms = 1s 
    //Times are offset from the server timestamp
    public TimeManager()
    {
        ResetEndTime();
    }
    public TimeManager(float _endTimeOffset)
    {
        ResetEndTime(_endTimeOffset);
    }
    public TimeManager(float _currentTime, float _endTime)
    {
        currentTime = _currentTime;
        endTime = _endTime;
    }
    public void AddToEndTime(float _offset)
    {
        
        endTime += _offset;
    }
    public void ResetEndTime()
    {
        ResetEndTime(0);
    }
    public void ResetEndTime(float _endTimeOffset)
    {
        UpdateCurrentTime();
        UpdateEndTime(currentTime + _endTimeOffset);
    }
    public void UpdateEndTime(float _newEndTime)
    {
        endTime = _newEndTime;
    }
    public float GetEndTime()
    {
        return endTime;
    }
    public void UpdateCurrentTime()
    {
        currentTime = PhotonNetwork.ServerTimestamp;
    }
    public float GetTimeLeft()
    {
        float remainingTime = endTime - currentTime;
        remainingTime = remainingTime > 0 ? remainingTime : 0;
        return remainingTime;
    }
    public float GetTimeLeftInSeconds()
    {
        return (GetTimeLeft() / 1000);
    }
    public void AddRandomOffsetToEndTime(float _maxRange) //_maxRange in Seconds
    {
        AddToEndTime((Random.Range(0, _maxRange)) * SECONDS_TO_MS);
    }
    public void AddRandomOffsetToEndTime(float _minRange, float _maxRange)//_maxRange and _minRange in Seconds
    {
        AddToEndTime((Random.Range(_minRange, _maxRange)) * SECONDS_TO_MS);
    }
    public bool IsTimeUp()
    {
        return GetTimeLeft() > 0 ? false : true;
    }
}
