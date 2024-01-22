using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] LayerMask pieceLayer;
    [SerializeField] MovementManager MM;
    [SerializeField] LevelManager LM;
    [SerializeField] float minDist;

    Transform objToMove;
    Transform finalPos;
    Transform saveObj;
    Vector3 savePos;
    int saveNChilds;
    bool canUndoMove;

    private void OnEnable()
    {
        FoodPiece.OnAutoUndoMove += UndoMove;
    }

    private void OnDisable()
    {
        FoodPiece.OnAutoUndoMove -= UndoMove;
    }

    private void Update()
    {
        if (Input.touches.Length == 0) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (!hit.transform.CompareTag("Ground"))
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    objToMove = hit.transform;
                }
                else if (Vector3.Distance(hit.point, objToMove.position) > minDist && objToMove != null)
                {
                    Vector3 dir = (hit.point - objToMove.position).normalized;
                    if (Mathf.Abs(dir.z) > Mathf.Abs(dir.x))
                    {
                        if (dir.z > 0) CheckMove(new Vector3(objToMove.position.x, 0, objToMove.position.z + 1));
                        else CheckMove(new Vector3(objToMove.position.x, 0, objToMove.position.z - 1));
                    }
                    else
                    {
                        if (dir.x > 0) CheckMove(new Vector3(objToMove.position.x + 1, 0, objToMove.position.z));
                        else CheckMove(new Vector3(objToMove.position.x - 1, 0, objToMove.position.z));
                    }
                }
            }
        }

        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            if (objToMove != null && finalPos != null)
            {
                if (MM.MovePiece(objToMove, finalPos)) SaveMove(objToMove, new Vector3(objToMove.position.x, 0, objToMove.position.z), objToMove.childCount);
                objToMove = null;
                finalPos = null;
            }
        }
    }

    void CheckMove(Vector3 finPos)
    {
        Vector3 finDir = finPos - Camera.main.transform.position;
        RaycastHit secHit;
        if (Physics.Raycast(Camera.main.transform.position, finDir, out secHit, Mathf.Infinity, pieceLayer))
        {
            finalPos = secHit.transform;
        }
    }

    void SaveMove(Transform obj, Vector3 pos, int childs)
    {
        canUndoMove = true;
        saveObj = obj;
        savePos = pos;
        saveNChilds = childs;
    }

    public void UndoMove()
    {
        if (canUndoMove)
        {
            MM.MovePiece(saveObj.parent != null ? saveObj.parent : saveObj, savePos, saveNChilds);
            canUndoMove = false;
        }
    }
}
