using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Balance;
    public int startBalance = 400;
    private void Start()
    {
        Balance = startBalance;
    }
}
