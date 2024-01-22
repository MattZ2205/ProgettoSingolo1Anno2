using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPiece : MonoBehaviour
{
    List<Transform> childs = new List<Transform>();

    public delegate void autoUndoMove();
    public static autoUndoMove OnAutoUndoMove;

    public void SetPiecesUndo(int nChild)
    {
        Transform obj = null;
        if (nChild < transform.childCount)
        {
            obj = transform.GetChild(nChild);
            obj.SetParent(null);

            for (int i = nChild; i < transform.childCount; i++)
            {
                transform.GetChild(i).SetParent(obj);
            }
        }
    }

    public void FinalizeChilds()
    {
        if (transform.childCount > 0)
        {
            Transform topObj = transform.GetChild(transform.childCount - 1);
            topObj.SetParent(null);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).SetParent(topObj);
            }
            transform.SetParent(topObj);
        }
    }

    public void SetPiecesOrder(Transform baseOfPile)
    {
        childs.Clear();

        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                childs.Add(transform.GetChild(i));

                if (transform.GetChild(i).childCount > 0)
                {
                    for (int j = 0; j < transform.GetChild(i).childCount; j++)
                    {
                        Transform child = transform.GetChild(i).GetChild(j);
                        child.SetParent(transform);
                        childs.Add(child);
                    }
                }
            }

            Transform top = childs[childs.Count - 1];
            childs.RemoveAt(childs.Count - 1);
            top.SetParent(null);
            top.position = new Vector3(baseOfPile.position.x, top.position.y, baseOfPile.position.z);

            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].SetParent(top, true);
            }
            transform.SetParent(top, true);
            baseOfPile.SetParent(top, true);
            if (baseOfPile.childCount > 0)
            {
                while (baseOfPile.childCount > 0)
                {
                    baseOfPile.GetChild(0).SetParent(top);
                }
            }

            if (top.name.Equals("Pane(Clone)"))
            {
                Transform bP = top.GetChild(top.childCount - 1);

                if (top.name.Equals("Pane(Clone)") && bP.name.Equals("Pane(Clone)") && top.childCount == GameManager._instance.nPieces - 1)
                {
                    GameManager._instance.Win();
                }
                else
                {
                    OnAutoUndoMove?.Invoke();
                }
            }
            return;
        }
        else if (baseOfPile.childCount != 0)
        {
            baseOfPile.SetParent(transform);
            while (baseOfPile.childCount > 0)
            {
                baseOfPile.GetChild(0).SetParent(transform);
            }
        }
        else
        {
            baseOfPile.SetParent(transform, true);
        }

        if (name.Equals("Pane(Clone)"))
        {
            Transform bP = transform.GetChild(transform.childCount - 1);

            if (name.Equals("Pane(Clone)") && bP.name.Equals("Pane(Clone)") && transform.childCount == GameManager._instance.nPieces - 1)
            {
                GameManager._instance.Win();
            }
            else
            {
                OnAutoUndoMove?.Invoke();
            }
        }
    }
}
