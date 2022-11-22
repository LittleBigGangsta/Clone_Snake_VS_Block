using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    
    public PlayerMovement Player;
    public Transform FinishPlatform;
    public Slider Slider;

    private float _startZ;
    private float _minimumReachedZ;

    private void Start()
    {
        _startZ = Player.transform.position.z;
    }

    private void Update()
    {
        float currentZ = Player.transform.position.z;
        float finishZ = FinishPlatform.transform.position.z;
        float t = Mathf.InverseLerp(_startZ, finishZ, currentZ);
        Slider.value = t;
    }
}
