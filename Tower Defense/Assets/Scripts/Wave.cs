using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    public GameObject enemy;
    public int count;
    public float spawningRate; //per second
    public int pathIndx; // the path index which the wave will traverse
}
