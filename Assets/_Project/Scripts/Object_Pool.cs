using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pool : MonoBehaviour
{
    public GameObject prefab;
    public int Amount;

    public GameObject[] Prefabs;

    private int index; 

    void Start()
    {
        Prefabs = new GameObject[Amount];

        for (int i = 0; i < Amount; i++)
        {
            Prefabs[i] = Instantiate(prefab, new Vector3(25, 25, 0), Quaternion.identity);
            Prefabs[i].SetActive(false);
        }
    }

    public GameObject GetObject()
    {

        index++;
        if (index >= Amount)
        {
            index = 0;
        }


        Prefabs[index].SetActive(true);
        return Prefabs[index];
    }
}
