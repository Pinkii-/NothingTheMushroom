using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    public GameObject mPotPrefab;
    public GameObject mMushroomPrefab;

    public int sizeX = 16;
    public int sizeY = 16;

    List<List<GameObject>> mPots;

    public float tileSize = 0.3f;

    PotScript potSelected = null;
    PotScript potOvered = null;


    private void SpawnMushroom(GameObject mushroomPrefab, int x, int y, MushroomBehaviour mushroomBehaviour, MushroomColor color)
    {
        GameObject mushroom = Instantiate(mushroomPrefab, mPots[x][y].transform);

        MushroomControlerDeVerdad mushCon = mushroom.GetComponent<MushroomControlerDeVerdad>();
        mushCon.mBoardController = this;
        mushCon.mPos = new Vector2Int(x, y);
        mushCon.mMushroomBehaviour = Instantiate<MushroomBehaviour>(mushroomBehaviour);
        mushCon.mMushroomBehaviour.mMushroomColor = color;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (potSelected != null && potOvered != null)
            {
                MushroomControlerDeVerdad mushroomSelected = potSelected.GetComponentInChildren<MushroomControlerDeVerdad>();
                MushroomControlerDeVerdad mushroomTarget = potOvered.GetComponentInChildren<MushroomControlerDeVerdad>();

                if (mushroomSelected != null)
                {
                    mushroomSelected.transform.parent = potOvered.transform;
                    mushroomSelected.transform.localPosition = Vector3.zero;
                    mushroomSelected.mPos = potOvered.GetComponent<PotScript>().mPos;
                }

                if (mushroomTarget != null)
                {
                    mushroomTarget.transform.parent = potSelected.transform;
                    mushroomTarget.transform.localPosition = Vector3.zero;
                    mushroomTarget.mPos = potSelected.GetComponent<PotScript>().mPos;
                }
            }
            else
            {
                if (potSelected != null)
                {
                    MushroomControlerDeVerdad mushroomSelected = potSelected.GetComponentInChildren<MushroomControlerDeVerdad>();
                    if (mushroomSelected != null)
                    {
                        mushroomSelected.transform.localPosition = Vector3.zero;
                    }
                }
            }


            potSelected = null;
            potOvered = null;
        }
    }

    public void OnPotDown(PotScript pot)
    {
        potSelected = pot;
        Debug.Log("PotDown");
    }

    public void OnPotOver(PotScript pot)
    {
        potOvered = pot;
        Debug.Log("PotOver");
    }

    public void InitBoard()
    {
        mPots = new List<List<GameObject>>();
        for (int i = 0; i < sizeX; ++i)
        {
            mPots.Add(new List<GameObject>());
            for (int j = 0; j < sizeY; ++j)
            {
                GameObject pot = Instantiate(mPotPrefab, this.transform);

                pot.transform.position = new Vector2(i * tileSize - 2, j * tileSize - 2);

                var potS = pot.GetComponent<PotScript>();
                potS.mBoardController = this;
                potS.mPos = new Vector2Int(i, j);

                mPots[i].Add(pot);
            }
        }
    }

    internal void SpawnRound(GameLogic.Round round)
    {
        Debug.Log("RRip");
        foreach (GameLogic.MushroomDescriptor mushroom in round.mMushroomsToSpawn)
        {
            Debug.Log("DMuuushsroom");
            SpawnMushroom(mMushroomPrefab, mushroom.mPos.x, mushroom.mPos.y, mushroom.mBehaviour, mushroom.mColor);
        }
    }

    internal bool IsCenter(Vector2Int pos)
    {
        return pos == new Vector2Int(sizeX/2, sizeY/2);
    }

    bool HasMushRoom(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x >= sizeX || pos.y < 0 || pos.y >= sizeY) return false;
        var mushController = mPots[pos.x][pos.y].GetComponentInChildren<MushroomControlerDeVerdad>();
        return mushController != null;
    }


    internal bool HasNeighboursManhatan(Vector2Int pos, int range)
    {
        for (int i = -range; i <= range; ++i)
        {
            for (int j = -range; j <= range; ++j)
            {
                if (i == 0 && j == 0) continue;
                if (Math.Abs(i) + Math.Abs(j) <= range)
                {
                    if (HasMushRoom(pos + new Vector2Int(i, j)))
                    {
                        return true;
                    }
                }

            }
        }
        return false;
    }

    internal int HasNeighboursRing(Vector2Int pos, int range)
    {
        int amount = 0;
        for (int i = -range; i <= range; ++i)
        {
            for (int j = -range; j <= range; ++j)
            {
                if (i == 0 && j == 0) continue;

                if (HasMushRoom(pos + new Vector2Int(i, j)))
                {
                    ++amount;
                }
            }
        }
        return amount;
    }

    internal int FriendsAtRow(int row)
    {
        int amount = 0;
        int i = row;
        for(int j = 0; j < sizeY; ++j)
        {
            if(HasMushRoom(new Vector2Int(i, j)))
            {
                ++amount;
            }
        }
        return amount-1;
    }

    internal int FriendsAtCol(int col)
    {
        int amount = 0;
        int j = col;
        for (int i = 0; i < sizeX; ++i)
        {
            if (HasMushRoom(new Vector2Int(i, j)))
            {
                ++amount;
            }
        }
        return amount-1;
    }

    internal bool IsBorder(Vector2Int pos)
    {
        return IsTopBorder(pos) || IsBotBorder(pos) || IsRightBorder(pos) || IsLeftBorder(pos);
    }

    internal bool IsTopBorder(Vector2Int pos)
    {
        return pos.y == 0;
    }

    internal bool IsBotBorder(Vector2Int pos)
    {
        return pos.y == sizeY-1;
    }

    internal bool IsRightBorder(Vector2Int pos)
    {
        return pos.x == sizeX-1;
    }

    internal bool IsLeftBorder(Vector2Int pos)
    {
        return pos.x == 0;
    }

}
