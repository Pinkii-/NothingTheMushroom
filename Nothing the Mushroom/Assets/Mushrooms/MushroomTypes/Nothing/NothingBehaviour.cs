using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NothingData", menuName = "Mushrooms/Behaviours/Nothing", order = 1)]
public class NothingBehaviour : MushroomBehaviour
{
    public override bool IsHappy(BoardController mBoardController, Vector2Int pos)
    {
        return mBoardController.IsCenter(pos);
    }
}
