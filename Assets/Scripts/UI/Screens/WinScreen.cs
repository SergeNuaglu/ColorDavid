using System.Collections;
using TMPro;
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
    [SerializeField] private FullGiftScreen _fullGiftScreen;
    [SerializeField] private TMP_Text _winSign;

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
        SetRandomWinSign();
        _showScreenRoutine = StartCoroutine(ShowScreen(waitingTime));
    }

    public override void Close()
    {
        base.Close();
        _confettiEffect.Stop();
        _gift.SetLastFullness();
    }

    private void SetRandomWinSign()
    {
        string[] signs = { "Great", "Nicely Done", "Awesome", "You've done well" };
        float minValue = -1f;
        float maxValue = signs.Length - 1;
        float random = Random.Range(minValue, maxValue);

        for (int i = 0; i < signs.Length; i++)
        {
            if (random <= i)
            {
                _winSign.text = signs[i];
                break;
            }
        }
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
