using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourPickerData", menuName = "Mushrooms/Behaviours/BehaviourPicker", order = 2)]


public class BehaviourPicker : MushroomBehaviour
{
    /*AFEGEIX ELS ELEMENTS > AL FINAL < SI NO VOLS REFER TOTS ELS DATAS DEL BOARDcONTROLLER*/
    public enum EBehaviour { 
        manhatan1,
        isCenter, 
        ring, 
        ringNeighbours1, ringNeighbours2, ringNeighbours3, 
        
        friendsAtSecondRingOnly, 
        noFriendsAtRing,
        
        friendsAtSameRow, noFriendsAtSameRow,
        friendsAtSameCol, noFriendsAtSameCol,

        friendsAtColOrRow, noFriendsAtColOrRow,
        friendsAtColAndRow,

        isAtBorder, isNotAtBorder

    }//^LLEGEIX EL COMENTARI DE DALT

    public EBehaviour behaviour = EBehaviour.manhatan1;

    public override bool IsHappy(BoardController mBoardController, Vector2Int pos)
    {

        switch (behaviour) {

            case (EBehaviour.manhatan1):
                return mBoardController.HasNeighboursManhatan(pos, 1);
                

            case (EBehaviour.isCenter):
                return mBoardController.IsCenter(pos);
                

            case (EBehaviour.ring):
                return mBoardController.HasNeighboursRing(pos, 1) > 0;
                

            case (EBehaviour.ringNeighbours1):
                return mBoardController.HasNeighboursRing(pos, 1) == 1;
                
            case (EBehaviour.ringNeighbours2):
                return mBoardController.HasNeighboursRing(pos, 1) == 2;
                
            case (EBehaviour.ringNeighbours3):
                return mBoardController.HasNeighboursRing(pos, 1) == 3;
                

            case (EBehaviour.friendsAtSecondRingOnly):
                return mBoardController.HasNeighboursRing(pos, 1) == 0 && mBoardController.HasNeighboursRing(pos, 2) != 0;
                   
            
            case (EBehaviour.noFriendsAtRing):
                return mBoardController.HasNeighboursRing(pos, 1) == 0;
                

            case (EBehaviour.friendsAtSameRow):
                return mBoardController.FriendsAtRow(pos.x) != 0;
                

            case (EBehaviour.friendsAtSameCol):
                return mBoardController.FriendsAtCol(pos.y) != 0;
                

            case (EBehaviour.noFriendsAtSameRow):
                return mBoardController.FriendsAtRow(pos.x) == 0;
                

            case (EBehaviour.noFriendsAtSameCol):
                return mBoardController.FriendsAtCol(pos.y) == 0;
                

            case (EBehaviour.friendsAtColOrRow):
                return mBoardController.FriendsAtCol(pos.y) != 0 || mBoardController.FriendsAtRow(pos.x) != 0;
                

            case (EBehaviour.noFriendsAtColOrRow):
                return mBoardController.FriendsAtRow(pos.x) == 0 && mBoardController.FriendsAtCol(pos.y) == 0;
                

            case (EBehaviour.friendsAtColAndRow):
                return mBoardController.FriendsAtCol(pos.y) != 0 && mBoardController.FriendsAtRow(pos.x) != 0;
                

            case (EBehaviour.isAtBorder):
                return mBoardController.IsBorder(pos);
                

            case (EBehaviour.isNotAtBorder):
                return !mBoardController.IsBorder(pos);
                

            
            default:
                break;

        }
        return mBoardController.HasNeighboursRing(pos, 1) != 0;
    }
}
