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

[CreateAssetMenu(menuName = "ScriptableObjects/Level", fileName = "Level")]
public class Level : ScriptableObject
{
    public List<Piece> pieces;
}
