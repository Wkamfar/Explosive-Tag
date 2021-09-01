using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TimeManager 
{
    private float currentTime;
    private float endTime;
    //Time in milliseconds 1000ms = 1s 
    //Times are offset from the server timestamp
    public TimeManager()
    {
        UpdateCurrentTime();
        endTime = currentTime;
    }
    public TimeManager(float _endTimeOffset)
    {
        UpdateCurrentTime();
        UpdateEndTime(currentTime + _endTimeOffset);
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
    public void UpdateEndTime(float _newEndTime)
    {
        endTime = _newEndTime;
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


}
