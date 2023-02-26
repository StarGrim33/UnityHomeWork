using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Alarm))]
public class AlarmVolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _playAlarm;

    private const float MIN_VOLUME = 0f;
    private const float MAX_VOLUME = 1f;
    private const float FADE_TIME = 1f;

    private Alarm _alarm;
    private Coroutine _volumeCoroutine;
    private float _currentVolume = 0f;

    private void Start()
    {
        _alarm = GetComponent<Alarm>();
        _alarm.PlayerEntered += OnPlayerEntered;
        _alarm.PlayerExited += OnPlayerExited;
    }

    private void OnDisable()
    {
        _alarm.PlayerEntered -= OnPlayerEntered;
        _alarm.PlayerExited -= OnPlayerExited;
    }

    public void OnPlayerExited()
    {
        FadeVolumeTarget(MIN_VOLUME);
    }

    public void OnPlayerEntered()
    {
        FadeVolumeTarget(MAX_VOLUME);
    }

    private IEnumerator FadeVolume(float startVolume, float endVolume)
    {
        while (startVolume != endVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, endVolume, FADE_TIME * Time.deltaTime);
            _playAlarm.volume = _currentVolume;
            yield return null;
        }

        _currentVolume = endVolume;
        _playAlarm.volume = _currentVolume;
    }
    
    private void FadeVolumeTarget(float volumeTarget)
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(FadeVolume(_currentVolume, volumeTarget));
    }
}
