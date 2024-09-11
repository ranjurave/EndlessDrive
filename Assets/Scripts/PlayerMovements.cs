using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PlayerMovements : MonoBehaviour {
    [SerializeField] AudioClip policeCry;
    [SerializeField] AudioClip petrolFill;
    [SerializeField] AudioClip coinCollect;
    [SerializeField] public float petrol = 100;
    [SerializeField] GameObject carBody;
    [SerializeField] float test;
    
    float fuelefficiency = 0.2f;
    float forwardSpeed = 10;
    float horizontalSpeed = 0.01f;
    float horizontalPCSpeed = 10f;
    float horizontalInput;
    float distanceThreshold = 50.0f;
    float speedRate = 5;
    public int demerits;
    public float travelledDistance;
    public bool gameBegin = true;
    public Renderer carRenderer;
    GameUI gameUI;

    Touch touch;
    new Rigidbody rigidbody;
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Start() {
        gameUI = FindFirstObjectByType<GameUI>();
        gameUI.StartGame();
        carRenderer = carBody.GetComponent<Renderer>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.G)) {
            carRenderer.material.EnableKeyword("_EMISSION");
            carRenderer.material.SetColor("_EmissionColor", Color.black);
        }

        if (!GameOver()) {
            PlayAudio();
            DistanceCheck();
            if (Application.platform == RuntimePlatform.Android) { HandleMobileInput(); }
            else { HandlePCInput(); }
            gameUI.UpdateUI(demerits, petrol, travelledDistance);
        }
        else {
            string gameOverMessage = " ";
            audioSource.Stop();
            if (demerits >= 100) { 
                demerits = 100;
                gameOverMessage = "You got 100 Demerits !!!";
            }
            if (petrol <= 0) {
                petrol = 0;
                gameOverMessage = "You ran out of fuel !!!";
            }
            gameUI.GameOverMenu(demerits, petrol, travelledDistance, gameOverMessage);
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
    public void Coin() {
        demerits -= 20;
        if (demerits < 0) { demerits = 0; }
        audioSource.PlayOneShot(coinCollect);
    }

    public void ResetColour() {
        StartCoroutine(DelayReset());
    }

    IEnumerator DelayReset() {
        yield return new WaitForSeconds(0.2f);
        carRenderer.material.EnableKeyword("_EMISSION");
        carRenderer.material.SetColor("_EmissionColor", Color.black);
    }
}
