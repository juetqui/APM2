using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCreditBar : MonoBehaviour
{
    [SerializeField] Button ShopBtn;
    [SerializeField] UIManager uiManager;
    [SerializeField] CreditComponent creditComp;
    [SerializeField] TextMeshProUGUI creditText;
    [SerializeField] PlayerPrefsController prefs;

    private void Start()
    {
        ShopBtn.onClick.AddListener(PullOutShop);
        creditComp.onCreditChanged += UpdateCredit;
        UpdateCredit(creditComp.Credit);
        prefs.onSavedPrefs += UpdateCredit;
    }

    private void UpdateCredit(int newCredit)
    {
        creditText.SetText(newCredit.ToString());
    }

    private void PullOutShop()
    {
        uiManager.SwithToShop();
    }
}
