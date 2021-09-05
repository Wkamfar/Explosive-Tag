using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawningController : MonoBehaviour
{
    public int maxIceCount;
    public int currentIceCount;
    private bool definedPlayer;
    public GameObject ice;
    public GameObject player;
    public GameObject[] ices;
    public GameObject SpawnIce(Transform location)
    {
        GameObject currentIce = Instantiate(ice, location.position, Quaternion.identity);
        currentIceCount++;
        Debug.Log("IceSpawningController.SpawnIce: spawning Ice");
        return currentIce;
    }
    public void DespawnIce(GameObject currentIce)
    {
        Debug.Log("IceSpawningController.DespawnIce: this happened");
        Destroy(currentIce);
        currentIceCount--;
    }
    public void DefinePlayer(GameObject _player)
    {
        player = _player;
        definedPlayer = true;
    }
    private void Update()
    {
        if (definedPlayer)
        {
            ices = GameObject.FindGameObjectsWithTag("Ice");
            if (currentIceCount > maxIceCount)
            {
                DespawnIce(ices[0]);
            }
        }

    }
}
