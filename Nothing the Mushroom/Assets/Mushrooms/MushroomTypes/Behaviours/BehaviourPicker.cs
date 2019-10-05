using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourPickerData", menuName = "Mushrooms/Behaviours/BehaviourPicker", order = 2)]


public class BehaviourPicker : MushroomBehaviour
{
    /*AFEGEIX ELS ELEMENTS AL DARRERA SI NO VOLS REFER TOTS ELS DATAS*/
    public enum EBehaviour { manhatan1, isCenter, ring }//^LLEGEIX AIXO

    public EBehaviour behaviour = EBehaviour.manhatan1;

    public override bool IsHappy(BoardController mBoardController, Vector2Int pos)
    {

        switch (behaviour) {

            case (EBehaviour.manhatan1):
                return mBoardController.HasNeighboursManhatan(pos, 1);
                break;

            case (EBehaviour.isCenter):
                return mBoardController.IsCenter(pos);
                break;

            case (EBehaviour.ring):
                return mBoardController.HasNeighboursRing(pos, 1);
                break;

            default:
                break;

        }
        return mBoardController.HasNeighboursRing(pos, 1);
    }
}
