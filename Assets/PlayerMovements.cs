using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {
    [SerializeField] AudioClip policeCry;
    [SerializeField] AudioClip petrolFill;
    [SerializeField] public float petrol = 100;
    //[SerializeField] public AudioListener audioListener;

    float fuelefficiency = 0.05f;
    float forwardSpeed = 10;
    float horizontalSpeed = 0.01f;
    float horizontalPCSpeed = 10f;
    float horizontalInput;
    float distanceThreshold = 50.0f;
    float speedRate = 5;
    public int demerits;
    public float travelledDistance;
    public bool gameBegin = true;

    Touch touch;
    Rigidbody rigidbody;
    AudioSource audioSource;
    GameUI gameUI;

    private void Awake() {
        //audioListener.enabled = false;
        audioSource = GetComponent<AudioSource>();
        //audioSource.enabled = false;
    }
        

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
        gameUI = FindObjectOfType<GameUI>();
    }

    void Update() {
        if (!GameOver()) {
            PlayAudio();
            //audioListener.enabled = true;
            DistanceCheck();
            HandleMobileInput();
            //HandlePCInput();
            gameUI.UpdateUI(demerits, petrol, travelledDistance);
        }
        else {
            //audioListener.enabled = false;
            audioSource.Stop();
            gameUI.GameOverMenu();
        }
    }

    private void PlayAudio() {
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    private void HandlePCInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalMove = transform.right * horizontalInput * horizontalPCSpeed * Time.deltaTime; // New Line
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.deltaTime;
        Vector3 carPosition = rigidbody.position + forwardMove + horizontalMove;
        rigidbody.MovePosition(new Vector3(Mathf.Clamp(carPosition.x, -3, 3), carPosition.y, carPosition.z));
        petrol -= forwardSpeed * fuelefficiency * Time.deltaTime;

    }

    void HandleMobileInput() {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved ) {
                horizontalInput = touch.deltaPosition.x * horizontalSpeed;
            }
            else {
                horizontalInput = 0;
            }
        }
        Vector3 horizontalMove = new Vector3(horizontalInput, 0, 0);
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.deltaTime;
        Vector3 carPosition = rigidbody.position + forwardMove + horizontalMove;
        rigidbody.MovePosition(new Vector3(Mathf.Clamp(carPosition.x, -3, 3), carPosition.y, carPosition.z));
        petrol -= forwardSpeed * fuelefficiency * Time.deltaTime;
    }

    bool GameOver() {
        if (demerits >= 100 || petrol <= 0) {
            return true;
        }
        else {
            return false;
        }
    }

    void DistanceCheck() {
        travelledDistance = Vector3.Distance(Vector3.zero, transform.position);
        if (travelledDistance > distanceThreshold) {
            distanceThreshold += distanceThreshold;
            forwardSpeed += speedRate;
        }
    }
    public void Petrol() {
        petrol += 5;
        if (petrol >= 100) { petrol = 100; }
        audioSource.PlayOneShot(petrolFill);
    }
    public void Demerit() {
        demerits += 10;
        audioSource.PlayOneShot(policeCry);
    }
}

