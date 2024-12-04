using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    [SerializeField] private CreditComponent _creditComponent;

    private void Awake()
    {
        _creditComponent.SetPrefs(this);
    }

    private void Start()
    {
        _creditComponent.onCreditChanged += Save;
    }

    private void Save(int newCredit)
    {
        PlayerPrefs.SetInt("Player_Credits", newCredit);
        return;
    }

    public int Load()
    {
        Debug.Log(PlayerPrefs.GetInt("Player_Credits", 30));
        return PlayerPrefs.GetInt("Player_Credits", 30);
    }
}
