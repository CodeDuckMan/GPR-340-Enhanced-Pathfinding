using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject sphere;

    public void SpawnObject()
    {
        Instantiate(sphere);
    }
}
