using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {
    [SerializeField] AudioClip policeCry;
    [SerializeField] AudioClip petrolFill;
    [SerializeField] AudioClip coinCollect;
    [SerializeField] public float petrol = 100;
    [SerializeField] GameUI gameUI;

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

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Start() {
        gameUI = FindObjectOfType<GameUI>();
        gameUI.StartGame();
    }

    void Update() {
        if (!GameOver()) {
            PlayAudio();
            DistanceCheck();
            if (Application.platform == RuntimePlatform.Android) { HandleMobileInput(); }
            else { HandlePCInput(); }

            gameUI.UpdateUI(demerits, petrol, travelledDistance);
        }
        else {
            audioSource.Stop();
            gameUI.GameOverMenu(demerits, petrol, travelledDistance);
        }
    }

    private void PlayAudio() {
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    private void HandlePCInput() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalMove = transform.right * horizontalInput * horizontalPCSpeed * Time.deltaTime; // New Line
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.deltaTime;
        Vector3 carPosition = rigidbody.position + forwardMove + horizontalMove;
        rigidbody.MovePosition(new Vector3(Mathf.Clamp(carPosition.x, -3, 3), carPosition.y, carPosition.z));
        petrol -= forwardSpeed * fuelefficiency * Time.deltaTime;
    }

    void HandleMobileInput() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) {
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
        if (demerits > 100 || petrol <= 0) {
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
    public void Coin() {
        demerits -= 5;
        audioSource.PlayOneShot(coinCollect);
    }
}

