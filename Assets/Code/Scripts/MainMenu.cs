using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameScene;

    [SerializeField] private Image transitionGameImg;
    [SerializeField] private Image transitionImg;
    [SerializeField] private CanvasGroup creditsCG;

    private void Start()
    {
        transitionGameImg.transform.DOLocalMoveX(-1920f, 1f).SetEase(Ease.InSine);
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
        transitionGameImg.transform.DOLocalMoveX(0f, 1f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(gameScene);
    }
    
    private IEnumerator TransitionToExit()
    {
        transitionImg.transform.DOLocalMoveX(0f, 1f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
    
    public void TransitionToCredits()
    {
        creditsCG.DOFade(1f, 1f);
        transitionImg.transform.DOLocalMoveX(0, 1f).SetEase(Ease.InSine);
    }
    
    public void TransitionFromCredits()
    {
        creditsCG.DOFade(0f, 1f);
        transitionImg.transform.DOLocalMoveX(-1920, 1f).SetEase(Ease.InSine);
    }

    public void SetImageInSpace(Vector3 newLocation)
    {
        transitionImg.transform.position = newLocation;
    }
}
