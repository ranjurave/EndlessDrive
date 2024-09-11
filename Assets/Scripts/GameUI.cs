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
    [SerializeField] TextMeshProUGUI kilometersCovered;
    [SerializeField] TextMeshProUGUI demeritsGot;
    [SerializeField] TextMeshProUGUI petrolLeft;
    [SerializeField] TextMeshProUGUI gameOverLabel;
    [SerializeField] TextMeshProUGUI gameOverMessage;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject restartMenu;

    public void StartGame() {
        Time.timeScale = 1;
        hud.SetActive(true);
        restartMenu.SetActive(false);
    }
    public void UpdateUI(int demerits, float petrol, float distanceTravelled) {
        demeritText.text = "Demerits : " + demerits.ToString();
        petrolImage.fillAmount = petrol / 100;
        kilometers.text = "Distance Covered : " + (distanceTravelled / 100).ToString("F2");
    }
    public void GameOverMenu(int demerits, float petrol, float distanceTravelled, string gameMessage) {
        restartMenu.SetActive(true);
        gameOverMessage.text = gameMessage;
        demeritsGot.text = "Demerit Points : " + demerits.ToString();
        kilometersCovered.text = "Distance covered : " + distanceTravelled.ToString("F2");
        petrolLeft.text = "Petrol left : " + petrol.ToString("F2");
        hud.SetActive(false);
        Time.timeScale = 0;
    }
    public void quitGame() {
        Application.Quit();
        Debug.Log("Quitting application");
    }
    public void LoadGame() {
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }
}
