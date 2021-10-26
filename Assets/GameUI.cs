using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI demeritText;
    [SerializeField] TextMeshProUGUI kilometers;
    [SerializeField] Image petrolImage;

    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject restartMenu;

    void Start() {
        MainMenu.SetActive(true);
        HUD.SetActive(false);
        restartMenu.SetActive(false);
        Time.timeScale = 0;
        
    }

    public void UpdateUI(int demerits, float petrol, float distanceTravelled) {
        demeritText.text = "Demerits : " + demerits.ToString();
        petrolImage.fillAmount = petrol / 100;
        kilometers.text = "Distance Covered : " + (distanceTravelled / 100).ToString("F2");
    }

    public void GameBegin() {
        Time.timeScale = 1;
        MainMenu.SetActive(false);
        HUD.SetActive(true);
    }

    public void GameOverMenu() {
        restartMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void startLevel() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void quitGame() {
        Application.Quit();
    }
}
