using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPiece : MonoBehaviour
{
    List<Transform> childs = new List<Transform>();

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
                for (int i = 0; i < baseOfPile.childCount; i++)
                {
                    baseOfPile.GetChild(i).SetParent(top);
                }
            }
        }
        else
        {
            baseOfPile.SetParent(transform, true);
        }
    }
}
