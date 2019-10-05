using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    public GameObject potPrefab;
    public GameObject mushroomPrefab;

    public int x = 16;
    public int y = 16;

    List<List<GameObject>> mPots;

    public float tileSize = 0.3f;

    PotScript potSelected = null;
    PotScript potOvered = null;

    void Start()
    {
        InitBoard();
        GameObject mushroom = Instantiate(mushroomPrefab, mPots[x / 2][y / 2].transform);

        MushroomControlerDeVerdad mushCon = mushroom.GetComponent<MushroomControlerDeVerdad>();
        mushCon.mBoardController = this;
        mushCon.mPos = new Vector2Int(x / 2, y / 2);
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
        for (int i = 0; i < x; ++i)
        {
            mPots.Add(new List<GameObject>());
            for (int j = 0; j < y; ++j)
            {
                GameObject pot = Instantiate(potPrefab, this.transform);

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
        return pos == new Vector2Int(x/2, y/2);
    }
}
