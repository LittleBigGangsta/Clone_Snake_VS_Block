using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Game_Manager Game_Manager;
    public Rigidbody Rigidbody;

    public float Speed;
    public float Sensivity;
    public Text AmountText;
    public Text LivesText;

    private AudioSource PickupSound;

    public PlayerMovement player;

    private Camera _mainCamera;
    private Rigidbody _rigidbody;
    private Vector3 _lastPos;
    private float _sidewaysSpeed;
    private int _amount;

    public List<Transform> _tails;
    [SerializeField] private float _bonesDistance;
    [SerializeField]private GameObject _tailPrefab;


    private void Awake()
    {
        PickupSound = GetComponent<AudioSource>(); 
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastPos = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _sidewaysSpeed = 0; 
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = _mainCamera.ScreenToViewportPoint(Input.mousePosition) - _lastPos;
            _sidewaysSpeed += delta.x * Sensivity;
            _lastPos = _mainCamera.ScreenToViewportPoint(Input.mousePosition); 
        }


    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_sidewaysSpeed) > 4) _sidewaysSpeed = 4 * Mathf.Sign(_sidewaysSpeed);
        _rigidbody.velocity = new Vector3(_sidewaysSpeed * 5, 0, Speed);

        _sidewaysSpeed = 0;
        MoveTail();
    }

    private void OnEnable()
    {
        _amount = Random.Range(3, 7);
        AmountText.text = _amount.ToString();
    }

    private void MoveTail()
    {
        float sqrDistance = Mathf.Sqrt(_bonesDistance);
        Vector3 previousPosition = transform.position;

        foreach (var bone in _tails)
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqrDistance)
            {
                Vector3 currentBonePosition = bone.position;
                bone.position = previousPosition;
                previousPosition = currentBonePosition;
            }
            else
            {
                break;
            }

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pickup")
        {
            for (int i = 0; i < _amount; i++)
            {
                Destroy(collision.gameObject);
                PickupSound.Play(); 

                var body = Instantiate(_tailPrefab);

                _tails.Add(body.transform);

            }


            if (player != null)
            {
                player.SetText(player._tails.Count);
            }

        }
    }

    public void SetText(int _amount)
    {
        LivesText.text = _amount.ToString(); 
    }

    public void TakeDamage()
    {
        int bodyChild = _tails.Count;

        if (bodyChild <= 1)
        {
            Die();
        }
        else
        {
            _tails.Remove(_tails[_tails.Count - 1]);
        }

        SetText(bodyChild - 1);
    }

    public void ReachFinish()
    {
        Game_Manager.OnPlayerReachedFinish();
        Rigidbody.velocity = Vector3.zero;
    }

    public void Die()
    {
        Game_Manager.OnPlayerDied();
        /*_lossParticleSystem.Play();*/
        Rigidbody.velocity = Vector3.zero;
    }
}
