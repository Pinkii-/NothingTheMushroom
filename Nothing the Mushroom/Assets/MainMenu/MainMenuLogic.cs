using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic : MonoBehaviour
{
    #region inspector properties
    [SerializeField]
    private Animator mainMenuAnimator = null;
    #endregion

    public enum FinishReasonType { None, PlayClicked };
    public FinishReasonType FinishReason { get; private set; } = FinishReasonType.None;

    public IEnumerator MainMenuCoroutine()
    {
        enabled = true;

        Debug.Log("lololo");

        FinishReason = FinishReasonType.None;
        yield return new WaitWhile(() => FinishReason == FinishReasonType.None);


        Debug.Log("lilili");

        enabled = false;
    }

    public void OnPlayClicked()
    {
        FinishReason = FinishReasonType.PlayClicked;
    }

    private void OnEnable()
    {
        mainMenuAnimator.SetBool("Visible", true);
    }

    private void OnDisable()
    {
        mainMenuAnimator.SetBool("Visible", false);
    }
}
