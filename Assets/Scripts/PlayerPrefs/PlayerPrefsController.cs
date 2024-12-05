using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    [SerializeField] private CreditComponent _creditComponent;

    private int _startCurrency = default;

    public delegate void OnSavedPrefs(int myCurrency);
    public event OnSavedPrefs onSavedPrefs;

    private void Awake()
    {
        if (_creditComponent != null)
            _creditComponent.SetPrefs(this);
    }

    private void Start()
    {
        if (_creditComponent != null)
            _creditComponent.onCreditChanged += Save;
    }

    public void Save(int newCredit)
    {
        PlayerPrefs.SetInt("Player_Credits", newCredit);
        return;
    }

    public void SaveForAdd(int reward)
    {
        int myCurrency = PlayerPrefs.GetInt("Player_Credits", _startCurrency) + reward;
        PlayerPrefs.SetInt("Player_Credits", myCurrency);
        
        onSavedPrefs?.Invoke(myCurrency);
        return;
    }

    public int Load()
    {
        return PlayerPrefs.GetInt("Player_Credits", _startCurrency);
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        onSavedPrefs?.Invoke(PlayerPrefs.GetInt("Player_Credits", _startCurrency));
    }

    public void Init(int startCurrency)
    {
        _startCurrency = startCurrency;
    }
}
