using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Controller : MonoBehaviour
{
    public static Level_Controller instance;

    public int BlockAmount = 6;

    public float DamageTime = 0.1f; 

    public Material EasyColor, MediumColor, HardColor;

    public float BlocksDistance = 25;

    private void Awake()
    {
        instance = this;
    }
}
