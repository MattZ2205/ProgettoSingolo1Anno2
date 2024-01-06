using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float duration;

    public void MovePiece(Transform objToMove, Transform destination)
    {
        Debug.Log(objToMove.name + " " + destination.name);
        StartCoroutine(MoveAndRotate(objToMove, destination));
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

        //destination.SetParent(objToMove, true);
        objToMove.GetComponent<FoodPiece>().SetPiecesOrder(destination);
    }
}
