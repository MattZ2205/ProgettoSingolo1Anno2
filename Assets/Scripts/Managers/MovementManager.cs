using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public void MovePiece(Transform objToMove, Transform destination)
    {
        Debug.Log(objToMove.name + " First " + objToMove.position);
        Debug.Log(destination.name + " Last " + destination.position);
    }
}
