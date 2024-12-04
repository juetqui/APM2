using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private int _maxStamina = 10;
    [SerializeField] private float _staminaCooldown = 10.0f;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _staminaText;

    private int _currentStamina = default;
    private bool _isRecharging = default;
    private float _timer = default;

    private DateTime _nextStaminaTime = default, _lastStaminaTime = default;

    private void Start()
    {
        Load();
        UpdateStaminaUI();
        StartCoroutine(AutoRechargeStamina());
    }

    private void Save()
    {
        PlayerPrefs.SetInt("CurrentStamina", _currentStamina);
        PlayerPrefs.SetString("NextStaminaTime", _nextStaminaTime.ToString());
        PlayerPrefs.SetString("LastStaminaTime", _lastStaminaTime.ToString());
        PlayerPrefs.Save();
    }

    private void Load()
    {
        _currentStamina = PlayerPrefs.GetInt("CurrentStamina", _maxStamina);
        _nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("NextStaminaTime"));
        _lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("LastStaminaTime"));
    }

    private DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date)) return DateTime.Now;
        else return DateTime.Parse(date);
    }

    public void UseStaminaButton(int stamina)
    {
        UseStamina(stamina);
    }

    public bool UseStamina(int stamina)
    {
        if (_currentStamina < stamina)
            return false;

        _currentStamina -= stamina;

        if (!_isRecharging && _currentStamina < _maxStamina)
        {
            _nextStaminaTime = DateTime.Now.AddSeconds(_staminaCooldown);
            StartCoroutine(AutoRechargeStamina());
        }

        UpdateStaminaUI();
        Save();
        return true;
    }

    public void RechargeStamina(int stamina)
    {
        _currentStamina += stamina;
        UpdateStaminaUI();
        Save();
    }

    public void UpdateTimerUI()
    {
        if (_currentStamina >= _maxStamina)
        {
            _timerText.text = "Stamina Fully Charged";
            return;
        }

        TimeSpan timeLeft = _nextStaminaTime - DateTime.Now;

        _timerText.text = timeLeft.Minutes.ToString("00") + ":" + timeLeft.Seconds.ToString("00");
    }
    public void UpdateStaminaUI()
    {
        _staminaText.text = _currentStamina.ToString() +  "/" + _maxStamina.ToString();
    }

    private IEnumerator AutoRechargeStamina()
    {
        _isRecharging = true;

        while (_currentStamina < _maxStamina)
        {
            DateTime currentTime = DateTime.Now;
            DateTime nextTime = _nextStaminaTime;

            bool staminaAdded = false;

            while (currentTime > nextTime)
            {
                if (_currentStamina >= _maxStamina) break;
                RechargeStamina(1);
                staminaAdded = true;

                if (_lastStaminaTime > nextTime)
                    nextTime = _lastStaminaTime;

                nextTime = nextTime.AddSeconds(_staminaCooldown);
            }

            if (staminaAdded)
            {
                _nextStaminaTime = nextTime;
                _lastStaminaTime = DateTime.Now;
            }

            UpdateTimerUI();
            Save();
            yield return new WaitForEndOfFrame();
        }

        UpdateTimerUI();
        _isRecharging = false;
    }
}
