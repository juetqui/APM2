using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private JoyStick _moveStick;
    [SerializeField] private JoyStick _shootStick;
    [SerializeField] private PlayerCreditBar _shopButton;
    [SerializeField] private Button _shopBackButton;

    private int _index = 0;
    
    public delegate void OnOpenCanvas(int canvasIndex);
    public event OnOpenCanvas onOpenCanvas = default;

    void Start()
    {
        onOpenCanvas?.Invoke(_index);
        _moveStick.onStickValueUpdated += UpdateEventMove;
    }

    private void Update()
    {
    }

    private void UpdateEvent()
    {
        _index++;
        onOpenCanvas?.Invoke(_index);
    }

    public void UpdateEventMove(Vector2 vector2)
    {
        _moveStick.onStickValueUpdated -= UpdateEventMove;
        _shopButton.onShopClicked += UpdateEventShop;
        UpdateEvent();
    }

    public void UpdateEventShop()
    {
        _shopButton.onShopClicked -= UpdateEventShop;
        _shopBackButton.onClick.AddListener(UpdateEventBuy);
        UpdateEvent();
    }

    public void UpdateEventBuy()
    {
        _shopBackButton.onClick.RemoveListener(UpdateEventBuy);
        _shootStick.onStickTaped += UpdateEventChange;
        UpdateEvent();
    }

    public void UpdateEventChange()
    {
        _shootStick.onStickTaped -= UpdateEventChange;
        _shootStick.onStickValueUpdated += UpdateEventShoot;
        UpdateEvent();
    }

    public void UpdateEventShoot(Vector2 vector2)
    {
        _shootStick.onStickValueUpdated -= UpdateEventShoot;
        UpdateEvent();
    }
}
