using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _colorMatchSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _hitBowlSound;
    [SerializeField] private Circle _circle;

    private AudioSource _audioSource;
    private List<Platform> _platforms = new List<Platform>();
    private Coroutine _waitingRoutine;
    private bool _isMuted;

    private void OnEnable()
    {
        _circle.AllColorsMatched += OnAllColorMatched;
    }

    private void OnDisable()
    {
        foreach (Platform platform in _platforms)
        {
            platform.ColorMatched -= OnColorMatched;
            platform.David.BowlHit -= OnBowlHit;
        }

        if (_waitingRoutine != null)
            StopCoroutine(_waitingRoutine);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Init(Platform platform)
    {
        _platforms.Add(platform);
        platform.ColorMatched += OnColorMatched;
        platform.David.BowlHit += OnBowlHit;
    }

    public void Play(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public bool SwitchIsMuted()
    {
        if (_isMuted)
        {
            AudioListener.volume = 1f;
            _isMuted = false;
        }
        else
        {
            AudioListener.volume = 0f;
            _isMuted = true;
        }

        return _isMuted;
    }

    private void OnBowlHit()
    {
        Play(_hitBowlSound);
    }

    private void OnColorMatched()
    {
        _waitingRoutine = StartCoroutine(Wait());
    }
    private void OnAllColorMatched()
    {
        Play(_winSound);
    }

    private IEnumerator Wait()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(0.5f);

        yield return waitingTime;
        Play(_colorMatchSound);
    }
}
