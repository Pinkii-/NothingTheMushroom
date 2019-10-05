using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ManhatanNeighboursBehaviour", menuName = "Mushrooms/Behaviours/ManhatanNeighbours", order = 2)]
public class ManhatanNeighboursBehaviour : MushroomBehaviour
{
    public override bool IsHappy(BoardController mBoardController, Vector2Int pos)
    {
        return mBoardController.HasNeighboursManhatan(pos, 1);
    }
}
