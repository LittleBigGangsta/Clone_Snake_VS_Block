using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksSpawn : MonoBehaviour
{
    public Block[] allBlocks;

    public Vector3 PositionRange;
    public GameObject BlocksGroup; 

    private Transform _player; 

    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;

        SetBlocks();
        Reposition();
    }

    void SetBlocks()
    {
        for (int i = 0; i < allBlocks.Length; i++)
        {
            allBlocks[i].SetAmount();
        }
    }

    void Reposition()
    {
        int blocksAmount = FindObjectsOfType<BlocksSpawn>().Length;

        transform.position = new Vector3(_player.position.x + (Level_Controller.instance.BlocksDistance * (blocksAmount - 1)), 7f, 0);

        /*BlocksGroup.transform.localPosition = new Vector3(Random.Range(PositionRange.z, PositionRange.z), 20.85f, 0);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Reposition();

            SetBlocks();
        }
    }
}
