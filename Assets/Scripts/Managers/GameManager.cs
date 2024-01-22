using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public delegate void comlpetedLevel();
    public static comlpetedLevel OnLevelCompleted;

    [HideInInspector] public int lvlInd;
    [HideInInspector] public int nPieces;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        if (PlayerPrefs.HasKey("lvlInd")) lvlInd = PlayerPrefs.GetInt("lvlInd"); 
    }

    public void Win()
    {
        OnLevelCompleted?.Invoke();
    }
}
