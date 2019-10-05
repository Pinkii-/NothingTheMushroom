using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomControlerDeVerdad : MonoBehaviour
{
    public BoardController mBoardController;

    public Vector2Int mPos;

    public MushroomBehaviour mMushroomBehaviour;
    
    public void SetPos(Vector2Int pos)
    {
        mPos = pos;
    }

    void Update()
    {
        mMushroomBehaviour.LittleHappyUpdate(this);
    }
}
