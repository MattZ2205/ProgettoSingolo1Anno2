using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public delegate void gameComplete();
    public static gameComplete OnGameComplete;

    public List<Level> levels;

    [HideInInspector] public List<Transform> instanciedPieces = new List<Transform>();

    private void Start()
    {
        int ind = GameManager._instance.lvlInd;

        if (ind >= levels.Count)
        {
            OnGameComplete?.Invoke();
            return;
        }

        GameManager._instance.nPieces = levels[ind].pieces.Count;

        for (int i = 0; i < levels[ind].pieces.Count; i++)
        {
            instanciedPieces.Add(Instantiate(levels[ind].pieces[i].obj, new Vector3(levels[ind].pieces[i].pos.x, 0, levels[ind].pieces[i].pos.y), Quaternion.identity));
        }
    }
}
