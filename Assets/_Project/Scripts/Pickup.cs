using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Text AmountText;

    private int _amount;

    private PlayerMovement _playerMovement;
    private PlayerMovement player;

    private void Awake()
    {
        /*_playerMovement.GetComponent<PlayerMovement>();*/
        /*player = GetComponent<PlayerMovement>();*/
    }

    private void OnEnable()
    {
        _amount = Random.Range(3, 7); 
        AmountText.text = _amount.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < _amount; i++)
            {
                Destroy(gameObject); 

                int index = collision.transform.childCount;
                /*GameObject body = Instantiate(_playerMovement._tailPrefab, collision.transform);*/
               /* body.transform.localPosition = new Vector3(0, -index, 0);

                _playerMovement._tails.Add(body.transform);
*/
            }


            if (player != null)
            {
                player.SetText(player._tails.Count);
            }

        }
    }
}
