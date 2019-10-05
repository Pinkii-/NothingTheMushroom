using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    public BoardController mBoardController;

    public void OnMouseOver()
    {
        mBoardController.OnPotOver(this);
    }

    public void OnMouseDown()
    {
        mBoardController.OnPotDown(this);
    }
}
