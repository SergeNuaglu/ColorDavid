using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : Screen
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private ParticleSystem _confettiEffect;
    [SerializeField] private LastLevelData _passedLevelNumber;
    [SerializeField] private Gift _gift;
    [SerializeField] FullGiftScreen _fullGiftScreen;

    private Coroutine _showScreenRoutine;
    private int _currentLevelBuildIndex;

    public event UnityAction NextLevelButtonClicked;

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(() => OnButtonClicked(NextLevelButtonClicked));
        _currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveAllListeners();
    }

    public override void Open()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(1.5f);
        _passedLevelNumber.Set(++_currentLevelBuildIndex);
        _confettiEffect.Play();
        _showScreenRoutine = StartCoroutine(ShowScreen(waitingTime));
    }

    public override void Close()
    {
        base.Close();
        _confettiEffect.Stop();
        _gift.SetLastFullness();
    }

    private IEnumerator ShowScreen(WaitForSeconds waitingTime)
    {
        yield return waitingTime;

        if (_gift.IsFull)
            _fullGiftScreen.Open();

        base.Open();
        StopCoroutine(_showScreenRoutine);
    }
}
