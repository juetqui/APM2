using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCreditBar : MonoBehaviour
{
    [SerializeField] Button ShopBtn;
    [SerializeField] UIManager uiManager;
    [SerializeField] CreditComponent creditComp;
    [SerializeField] TextMeshProUGUI creditText;

    public delegate void OnShopClicked();
    public event OnShopClicked onShopClicked;

    private void Start()
    {
        ShopBtn.onClick.AddListener(PullOutShop);
        creditComp.onCreditChanged += UpdateCredit;
        UpdateCredit(creditComp.Credit);
    }

    private void UpdateCredit(int newCredit)
    {
        creditText.SetText(newCredit.ToString());
    }

    private void PullOutShop()
    {
        onShopClicked?.Invoke();
        uiManager.SwithToShop();
    }
}
