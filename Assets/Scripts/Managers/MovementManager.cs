using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float duration;

    Coroutine moveC;

    public bool MovePiece(Transform objToMove, Transform destination)
    {
        if (moveC != null) return false;
        moveC = StartCoroutine(MoveAndRotate(objToMove, destination));
        return true;
    }

    public bool MovePiece(Transform objToMove, Vector3 destination, int nChild)
    {
        if (moveC != null) return false;
        StartCoroutine(MoveAndRotate(objToMove, destination, nChild));
        return true;
    }

    IEnumerator MoveAndRotate(Transform objToMove, Vector3 destination, int nChild)
    {
        objToMove.GetComponent<FoodPiece>().SetPiecesUndo(nChild);

        objToMove.DOJump(new Vector3(destination.x, 0, destination.z), jumpForce, 1, duration);

        if (objToMove.position.x < destination.x) objToMove.DORotate(new Vector3(0, 0, -180f), duration, RotateMode.WorldAxisAdd);
        else if (objToMove.position.x > destination.x) objToMove.DORotate(new Vector3(0, 0, 180f), duration, RotateMode.WorldAxisAdd);
        else if (objToMove.position.z < destination.z) objToMove.DORotate(new Vector3(180f, 0, 0), duration, RotateMode.WorldAxisAdd);
        else if (objToMove.position.z > destination.z) objToMove.DORotate(new Vector3(-180f, 0, 0), duration, RotateMode.WorldAxisAdd);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        objToMove.GetComponent<FoodPiece>().FinalizeChilds();

        moveC = null;
    }

    IEnumerator MoveAndRotate(Transform objToMove, Transform destination)
    {
        objToMove.DOJump(new Vector3(destination.position.x, destination.position.y + (objToMove.localScale.y / 2 + destination.localScale.y / 2), destination.position.z), jumpForce, 1, duration);

        if (objToMove.position.x < destination.position.x) objToMove.DORotate(new Vector3(0, 0, -180f), duration, RotateMode.WorldAxisAdd);
        else if (objToMove.position.x > destination.position.x) objToMove.DORotate(new Vector3(0, 0, 180f), duration, RotateMode.WorldAxisAdd);
        else if (objToMove.position.z < destination.position.z) objToMove.DORotate(new Vector3(180f, 0, 0), duration, RotateMode.WorldAxisAdd);
        else if (objToMove.position.z > destination.position.z) objToMove.DORotate(new Vector3(-180f, 0, 0), duration, RotateMode.WorldAxisAdd);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        objToMove.GetComponent<FoodPiece>().SetPiecesOrder(destination);

        moveC = null;
    }
}
