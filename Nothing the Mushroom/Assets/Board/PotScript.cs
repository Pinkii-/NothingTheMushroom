using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    public BoardController mBoardController;
    public Vector2Int mPos;
    Color overColor = new Color(1f, 0.66f, 0.2f);
    Color originalColor = new Color(1f, 0.46f, 0f);

    public void OnMouseOver()
    {
        mBoardController.OnPotOver(this);
    }

    public void OnMouseEnter()
    {
        GetComponentInChildren<SpriteRenderer>().color = overColor;
    }

    public void OnMouseExit()
    {
        GetComponentInChildren<SpriteRenderer>().color = originalColor;
        mBoardController.OnPotOver(null);

    }

    public void OnMouseDown()
    {
        mBoardController.OnPotDown(this);
    }
    
}
