using System;
using UnityEngine;

public abstract class MushroomBehaviour : ScriptableObject
{
    private bool mWasHappy = true;
    private bool firtUpdate = true;

    public MushroomColor mMushroomColor;

    public abstract bool IsHappy(BoardController boardController, Vector2Int pos);

    internal void LittleHappyUpdate(MushroomControlerDeVerdad mushroom)
    {
        bool isHappy = IsHappy(mushroom.mBoardController, mushroom.mPos);
        if (isHappy && (!mWasHappy || firtUpdate))
        {
            mushroom.GetComponent<Animator>().runtimeAnimatorController = mMushroomColor.mHappyAnimator;
        }
        else if (!isHappy && (mWasHappy || firtUpdate))
        {
            mushroom.GetComponent<Animator>().runtimeAnimatorController = mMushroomColor.mSadAnimator;
        }

        mWasHappy = isHappy;
        firtUpdate = false;
    }

    internal bool IsHappy()
    {
        return mWasHappy;
    }
}
