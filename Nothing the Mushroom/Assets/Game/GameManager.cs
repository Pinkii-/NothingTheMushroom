﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region inspector properties
    [SerializeField] private MainMenuLogic mainMenuLogic;
    [SerializeField] private GameLogic gameLogic;
    #endregion

    void Start()
    {
        StartCoroutine(MainFlowCoroutine());
    }

    IEnumerator MainFlowCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(mainMenuLogic.MainMenuCoroutine());

            if (mainMenuLogic.FinishReason == MainMenuLogic.FinishReasonType.PlayClicked)
            {
                yield return StartCoroutine(gameLogic.InGameCoroutine());
            }
        }
    }
}