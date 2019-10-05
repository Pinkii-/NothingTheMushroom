using System;
using UnityEngine;

public abstract class MushroomBehaviour : ScriptableObject
{
    private bool mWasHappy = true;

    public RuntimeAnimatorController mHappyAnimator;
    public RuntimeAnimatorController mSadAnimator;

    public abstract bool IsHappy(BoardController boardController, Vector2Int pos);

    internal void LittleHappyUpdate(MushroomControlerDeVerdad mushroom)
    {
        bool isHappy = IsHappy(mushroom.mBoardController, mushroom.mPos);
        if (isHappy && !mWasHappy)
        {
            mushroom.GetComponent<Animator>().runtimeAnimatorController = mHappyAnimator;
        }
        else if (!isHappy && mWasHappy)
        {
            mushroom.GetComponent<Animator>().runtimeAnimatorController = mSadAnimator;
        }

        mWasHappy = isHappy;
    }
}
