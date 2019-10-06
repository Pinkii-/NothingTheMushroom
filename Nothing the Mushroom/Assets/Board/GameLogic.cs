using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public enum StateType { None, Playing, AnimatingNextRound, Finished };

    public class MushroomDescriptor
    {
        public Vector2Int mPos;
        public MushroomBehaviour mBehaviour;
        public MushroomColor mColor;
    }

    public class Round
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
    public int numColors = 4;
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
        List<MushroomDescriptor> mushroomBases = new List<MushroomDescriptor>();
        System.Random random = new System.Random();

        { // Generate Color and behaviour links
            for (int i = 0; i < numColors; ++i) mushroomBases.Add(new MushroomDescriptor());
        
            List<int> picked = new List<int>();

            for (int i = 1; i < mMushroomColors.Count; ++i) picked.Add(i);

            for (int i = 0; i < numColors; ++i)
            {
                int index = random.Next(picked.Count);
                mushroomBases[i].mColor = mMushroomColors[picked[index]];

                picked.Remove(index);
            }

            picked = new List<int>();

            for (int i = 1; i < mMushroomBehaviours.Count; ++i) picked.Add(i);

            for (int i = 0; i < numColors; ++i)
            {
                int index = random.Next(picked.Count);
                mushroomBases[i].mBehaviour = mMushroomBehaviours[picked[index]];

                picked.Remove(index);
            }
        }

        mRounds = new List<Round>();

        { // Round Nothing
            Round round = new Round();
            round.mMushroomsToSpawn = new List<MushroomDescriptor>();

            MushroomDescriptor nothingMush = GetDescriptorFor(0, 0, mMushroomBehaviours[0], mMushroomColors[0]);
            round.mMushroomsToSpawn.Add(nothingMush);

            mRounds.Add(round);
        }

        { // Round 1
            Round round = new Round();
            round.mMushroomsToSpawn = new List<MushroomDescriptor>();

            int numMush = 2;
            for (int i = 0; i < numMush; ++i)
            {
                var mushBase = mushroomBases[random.Next(numColors)];

                MushroomDescriptor descriptor = GetDescriptorFor(0, 0, mushBase.mBehaviour, mushBase.mColor);
                round.mMushroomsToSpawn.Add(descriptor);
            }

            mRounds.Add(round);
        }

        { // Round 2
            Round round = new Round();
            round.mMushroomsToSpawn = new List<MushroomDescriptor>();

            int numMush = 4;
            for (int i = 0; i < numMush; ++i)
            {
                var mushBase = mushroomBases[random.Next(numColors)];

                MushroomDescriptor descriptor = GetDescriptorFor(0, 0, mushBase.mBehaviour, mushBase.mColor);
                round.mMushroomsToSpawn.Add(descriptor);
            }

            mRounds.Add(round);
        }

        { // Round 3
            Round round = new Round();
            round.mMushroomsToSpawn = new List<MushroomDescriptor>();

            int numMush = 8;
            for (int i = 0; i < numMush; ++i)
            {
                var mushBase = mushroomBases[random.Next(numColors)];

                MushroomDescriptor descriptor = GetDescriptorFor(0, 0, mushBase.mBehaviour, mushBase.mColor);
                round.mMushroomsToSpawn.Add(descriptor);
            }

            mRounds.Add(round);
        }
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

        List<Vector2Int> mPosUsed = new List<Vector2Int>();

        foreach (MushroomDescriptor mush in mRounds[mCurrentRound].mMushroomsToSpawn)
        {
            Vector2Int pos = mBoardController.GetEmptyCell();
            while (mPosUsed.Contains(pos))
            {
                pos = mBoardController.GetEmptyCell();
            }
            mush.mPos = pos;
            mPosUsed.Add(pos);
        }

        mBoardController.SpawnRound(mRounds[mCurrentRound]);
    }

    private bool HasMoreRounds()
    {
        return mCurrentRound < mRounds.Count;
    }

    private bool IsEveryOneHappy()
    {
        return mBoardController.IsEveryoneHappy();
    }
}
