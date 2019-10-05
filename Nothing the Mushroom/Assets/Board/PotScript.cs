using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    public BoardController mBoardController;
    public Vector2Int mPos;
    Color overColor = new Color(1f, 0.66f, 0.2f);
    Color originalColor = new Color(1f, 0.46f, 0f);


    public void OnMouseEnter()
    {
        mBoardController.OnPotOver(this);
        GetComponentInChildren<SpriteRenderer>().color = overColor;
    }

    public void OnMouseExit()
    {
        GetComponentInChildren<SpriteRenderer>().color = originalColor;
        mBoardController.OnPotOver(null);
    }

    public void OnMouseDown()
    {
        GetComponentInChildren<DragAndDropMushroom>()?.StartDrag();
        mBoardController.OnPotDown(this);
    }

    private void OnMouseDrag()
    {
        GetComponentInChildren<DragAndDropMushroom>()?.OnMouseDrag();
    }

}
