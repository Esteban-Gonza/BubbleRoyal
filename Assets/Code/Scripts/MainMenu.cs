using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameScene;

    [SerializeField] private GameObject mainMenuOB;
    [SerializeField] private Transform transitionTransform;
    [SerializeField] private Transform instructionsTransform;
    [SerializeField] private CanvasGroup creditsCG;

    private void Start()
    {
        transitionTransform.DOLocalMoveX(-1920f, 1f).SetEase(Ease.InSine);
    }

    public void Exit()
    {
        StartCoroutine(TransitionToExit());
    }

    public void PlayGame()
    {
        StartCoroutine(TransitionToGame());
    }

    private IEnumerator TransitionToGame()
    {
        transitionTransform.DOLocalMoveX(0f, 1f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(1f);
        mainMenuOB.SetActive(false);
        transitionTransform.DOLocalMoveX(-1920f, 1f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(1f);
        instructionsTransform.DOLocalMoveX(960, 2f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(6f);
        instructionsTransform.DOLocalMoveX(-960, 1.5f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(5f);
        instructionsTransform.DOLocalMoveY(-1920, 1.5f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(gameScene);
    }
    
    private IEnumerator TransitionToExit()
    {
        transitionTransform.DOLocalMoveX(0f, 1f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
    
    public void TransitionToCredits()
    {
        creditsCG.DOFade(1f, 1f);
        transitionTransform.DOLocalMoveX(0, 1f).SetEase(Ease.InSine);
    }
    
    public void TransitionFromCredits()
    {
        creditsCG.DOFade(0f, 1f);
        transitionTransform.DOLocalMoveX(-1920, 1f).SetEase(Ease.InSine);
    }
}
