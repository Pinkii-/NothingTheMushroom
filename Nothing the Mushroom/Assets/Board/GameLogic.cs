using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public enum StateType { None, Playing, AnimatingNextRound, Finished };

    public struct MushroomDescriptor
    {
        public Vector2Int mPos;
        public MushroomBehaviour mBehaviour;
        public MushroomColor mColor;
    }

    public struct Round
    {
        public List<MushroomDescriptor> mMushroomsToSpawn;
    }

    #region inspector properties
    [Header("References")]
    [SerializeField] private List<MushroomBehaviour> mMushroomBehaviours = null;
    [SerializeField] private List<MushroomColor> mMushroomColors = null;
    [SerializeField] private BoardController mBoardController = null;
    [Header("BoardSetup")]
    public Vector2Int mBoardSize;
    public int maxRounds = 10;
    #endregion

    public StateType mState { get; private set; } = StateType.None;

    private int mCurrentRound = 0;

    private List<Round> mRounds;


    public IEnumerator InGameCoroutine()
    {
        enabled = true;
        mState = StateType.None;

        mCurrentRound = 0;
        SetupGame();

        while (HasMoreRounds() && mState != StateType.Finished)
        {
            SetupRound();

            // mState = StateType.AnimatingNextRound;


            // Present Round
            yield return new WaitWhile(() => mState == StateType.AnimatingNextRound);

            mState = StateType.Playing;

            // Play Round
            yield return new WaitWhile(() => !IsEveryOneHappy() && mState == StateType.Playing);

            mCurrentRound += 1;
        }

        // Win or lose
        mState = StateType.Finished;
  
        enabled = false;
    }

    private void SetupGame()
    {
        // TODO: Destroy the board or recreate the board being this a prefab
        mBoardController.sizeX = mBoardSize.x;
        mBoardController.sizeY = mBoardSize.y;
        mBoardController.InitBoard();

        GenereateRoundForGame();
    }

    private void GenereateRoundForGame()
    {
        mRounds = new List<Round>();


        Round round = new Round();
        round.mMushroomsToSpawn = new List<MushroomDescriptor>();

        MushroomDescriptor descriptor0 = GetDescriptorFor(mBoardSize.x / 2, mBoardSize.y / 2, mMushroomBehaviours[1], mMushroomColors[1]);
        MushroomDescriptor descriptor1 = GetDescriptorFor(mBoardSize.x / 3, mBoardSize.y / 5, mMushroomBehaviours[0], mMushroomColors[0]);
        MushroomDescriptor descriptor2 = GetDescriptorFor(mBoardSize.x / 4, mBoardSize.y / 4, mMushroomBehaviours[1], mMushroomColors[2]);
        MushroomDescriptor descriptor3 = GetDescriptorFor(mBoardSize.x / 5, mBoardSize.y / 3, mMushroomBehaviours[1], mMushroomColors[3]);

        round.mMushroomsToSpawn.Add(descriptor0);
        round.mMushroomsToSpawn.Add(descriptor1);
        round.mMushroomsToSpawn.Add(descriptor2);
        round.mMushroomsToSpawn.Add(descriptor3);

        mRounds.Add(round);
    }

    private MushroomDescriptor GetDescriptorFor(int x, int y, MushroomBehaviour mushroomBehaviour, MushroomColor mushroomColor)
    {
        MushroomDescriptor mushroomDescriptor = new MushroomDescriptor();
        mushroomDescriptor.mPos = new Vector2Int(x, y);
        mushroomDescriptor.mBehaviour = mushroomBehaviour;
        mushroomDescriptor.mColor = mushroomColor;

        return mushroomDescriptor;
    }

    private void SetupRound()
    {
        // Present next round to the player with fancy animations :D
        mBoardController.SpawnRound(mRounds[mCurrentRound]);
    }

    private bool HasMoreRounds()
    {
        return mCurrentRound < mRounds.Count;
    }

    private bool IsEveryOneHappy()
    {
        // TODO
        return false;
    }
}
