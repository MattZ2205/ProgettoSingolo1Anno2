using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] LayerMask pieceMask;
    [SerializeField] MovementManager MM;

    Transform objToMove;
    Transform finalPos;

    private void Update()
    {
        if (Input.touches.Length == 0) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, pieceMask))
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                objToMove = hit.transform;
            }
            else /*if (Input.touches[0].phase == TouchPhase.Ended)*/
            {
                if (CheckMove(hit.transform))
                {
                    finalPos = hit.transform;
                }
            }
        }

        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            if (finalPos != null) MM.MovePiece(objToMove, finalPos);
            objToMove = null;
            finalPos = null;
        }
    }

    bool CheckMove(Transform hitted)
    {
        if (Mathf.Abs(Mathf.Abs(objToMove.position.x) - Mathf.Abs(hitted.position.x)) == 1f && Mathf.Abs(Mathf.Abs(objToMove.position.z) - Mathf.Abs(hitted.position.z)) == 0f) return true;
        if (Mathf.Abs(Mathf.Abs(objToMove.position.x) - Mathf.Abs(hitted.position.x)) == 0f && Mathf.Abs(Mathf.Abs(objToMove.position.z) - Mathf.Abs(hitted.position.z)) == 1f) return true;
        return false;
    }
}
