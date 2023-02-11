using Lean.Localization;
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
    [SerializeField] private Gift _gift;
    [SerializeField] private FullGiftScreen _fullGiftScreen;
    [SerializeField] private TMP_Text _winSign;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedText;

    private Coroutine _showScreenRoutine;
    private const string PassedLevelKey = nameof(PassedLevelKey);
    private int _currentLevelNumber;

    public event UnityAction NextLevelButtonClicked;

    private void Start()
    {
        var currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;
        _currentLevelNumber = currentLevelBuildIndex + 1;

        if (PlayerPrefs.HasKey(PassedLevelKey) == false)
            PlayerPrefs.SetInt(PassedLevelKey, _currentLevelNumber);
    }

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(() => OnButtonClicked(NextLevelButtonClicked));
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveAllListeners();
    }

    public override void Open()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(1.5f);

        if (_currentLevelNumber > PlayerPrefs.GetInt(PassedLevelKey))
            PlayerPrefs.SetInt(PassedLevelKey, _currentLevelNumber);

        _confettiEffect.Play();
        SetRandomWinSign();
        _showScreenRoutine = StartCoroutine(ShowScreen(waitingTime));
    }

    public override void Close()
    {
        base.Close();
        _confettiEffect.Stop();
    }

    private void SetRandomWinSign()
    {
        string[] signs = { "Great", "Nicely Done", "Awesome", "You've done well", "Bravo" };
        float minValue = -1f;
        float maxValue = signs.Length - 1;
        float random = Random.Range(minValue, maxValue);
        LeanTranslation translation;

        for (int i = 0; i < signs.Length; i++)
        {
            if (random <= i)
            {
                if(LeanLocalization.CurrentTranslations.TryGetValue(signs[i], out translation))
                    _localizedText.UpdateTranslation(translation);

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
