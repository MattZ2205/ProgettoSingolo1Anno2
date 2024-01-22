using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform winCanvas;
    [SerializeField] Transform endGameCanvas;
    [SerializeField] Button goBackButton;

    private void OnEnable()
    {
        GameManager.OnLevelCompleted += ActivateWinCanvas;
        LevelManager.OnGameComplete += ActivateEndGameCanvas;
    }

    private void OnDisable()
    {
        GameManager.OnLevelCompleted -= ActivateWinCanvas;
        LevelManager.OnGameComplete -= ActivateEndGameCanvas;
    }

    void ActivateWinCanvas()
    {
        goBackButton.enabled = false;
        winCanvas.gameObject.SetActive(true);
    }

    void ActivateEndGameCanvas()
    {
        goBackButton.enabled = false;
        endGameCanvas.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("lvlInd", GameManager._instance.lvlInd + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
