using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Piece
{
    public Transform obj;
    public Vector2 pos;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Piece> pieces;

    private void Start()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            Transform ob = Instantiate(pieces[i].obj, new Vector3(pieces[i].pos.x, 0, pieces[i].pos.y), Quaternion.identity);
        }
    }
}
