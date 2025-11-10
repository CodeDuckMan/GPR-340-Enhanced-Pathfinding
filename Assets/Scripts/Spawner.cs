using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject newObject;

    public void SpawnObject()
    {
        Instantiate(newObject);
    }
}
