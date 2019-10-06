using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region inspector properties
    [SerializeField] private MainMenuLogic mainMenuLogic = null;
    [SerializeField] private GameLogic gameLogic = null;
    #endregion

    void Start()
    {
        mainMenuLogic.enabled = false;
        gameLogic.enabled = false;

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
