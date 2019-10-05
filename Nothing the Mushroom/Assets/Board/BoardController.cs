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
        mPots = new List<List<GameObject>>();
        for (int i = 0; i < x; ++i)
        {
            mPots.Add(new List<GameObject>());
            for (int j = 0; j < y; ++j)
            {
                GameObject pot = Instantiate(potPrefab, this.transform);

                pot.transform.position = new Vector2(i * tileSize - 2, j * tileSize - 2);

                pot.GetComponent<PotScript>().mBoardController = this;

                mPots[i].Add(pot);
            }
        }

        GameObject mushroom = Instantiate(mushroomPrefab, mPots[x/2][y/2].transform);
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (potSelected != null && potOvered != null)
            {
                MushroomCon mushroomSelected = potSelected.GetComponentInChildren<MushroomCon>();
                MushroomCon mushroomTarget = potOvered.GetComponentInChildren<MushroomCon>();

                if (mushroomSelected != null)
                {
                    mushroomSelected.transform.parent = potOvered.transform;
                    mushroomSelected.transform.localPosition = Vector3.zero;
                }

                if (mushroomTarget != null)
                {
                    mushroomTarget.transform.parent = potSelected.transform;
                    mushroomTarget.transform.localPosition = Vector3.zero;
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
}
