﻿using System;
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

    public List<MushroomBehaviour> mushroomBehaviours;
    public List<MushroomColor> mushroomColors;

    void Start()
    {
        InitBoard();

        SpawnMushroom(mMushroomPrefab, sizeX / 2, sizeY / 2, mushroomBehaviours[1], mushroomColors[1]);
        SpawnMushroom(mMushroomPrefab, sizeX / 3, sizeY / 5, mushroomBehaviours[0], mushroomColors[0]);
        SpawnMushroom(mMushroomPrefab, sizeX / 4, sizeY / 4, mushroomBehaviours[1], mushroomColors[2]);
        SpawnMushroom(mMushroomPrefab, sizeX / 5, sizeY / 3, mushroomBehaviours[1], mushroomColors[3]);
    }

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

    private void InitBoard()
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

    internal bool HasNeighboursRing(Vector2Int pos, int range)
    {
        for (int i = -range; i <= range; ++i)
        {
            for (int j = -range; j <= range; ++j)
            {
                if (i == 0 && j == 0) continue;

                if (HasMushRoom(pos + new Vector2Int(i, j)))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
