using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PlayerMovement; 

public class Block : MonoBehaviour
{
    public Text AmountText;

    public ParticleSystem Destroy_Particle; 

    public Game_Manager Game_Manager;

    private PlayerMovement _playerMovement;
    private float _nextTime;

    private int _score; 
    private int _amount;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        SetAmount();
    }

    void Update()
    {
        SetColor();

        if (_playerMovement != null && _nextTime < Time.time)
        {
            PlayerDamage();
        }
    }

    public void SetAmount()
    {
        gameObject.SetActive(true);
        _amount = Random.Range(0, Level_Controller.instance.BlockAmount); 
        if (_amount <= 0)
        {
            gameObject.SetActive(false);
        }

        SetAmountText();
        SetColor();
    }

    public void SetAmountText()
    {
        AmountText.text = _amount.ToString();
    }

    public void SetColor()
    {
        /*int playerLives = FindObjectOfType<PlayerMovement>()._tails.Count;*/
        int _playerLives = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>()._tails.Count;
        Material newColor; 

        if (_amount > _playerLives)
        {
            newColor = Level_Controller.instance.HardColor;
        }
        else if (_amount > _playerLives / 2)
        {
            newColor = Level_Controller.instance.MediumColor;
        }
        else
        {
            newColor = Level_Controller.instance.EasyColor;
        }

        _meshRenderer.sharedMaterial = newColor; 
    }

    void PlayerDamage()
    {
        _nextTime = Time.time + Level_Controller.instance.DamageTime;
        _playerMovement.TakeDamage();
        _amount--;
        SetAmountText();
        if (_amount <= 0)
        {
            gameObject.SetActive(false);
            Destroy_Particle.Play();
            _playerMovement = null;
        }
        else if (Game_Manager.currentState == Game_Manager.State.Loss)
        {
            _amount++;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(DamageColor());
        }

    }

    IEnumerator DamageColor()
    {
        float timer = 0;
        float t = 0;

        while (timer < Level_Controller.instance.DamageTime)
        {
            timer += Time.deltaTime;
            t += Time.deltaTime / Level_Controller.instance.DamageTime;
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement otherCollision = collision.gameObject.GetComponent<PlayerMovement>(); 
        if (otherCollision != null)
        {
            _playerMovement = otherCollision; 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerMovement otherCollision = collision.gameObject.GetComponent<PlayerMovement>();
        if (otherCollision != null)
        {
            _playerMovement = null; 
        }
    }
}
