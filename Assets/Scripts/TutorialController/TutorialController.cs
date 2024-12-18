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
        _shopBackButton.onClick.AddListener(UpdateEvent);
    }

    private void UpdateEvent()
    {
        _index++;
        onOpenCanvas?.Invoke(_index);
    }

    public void UpdateEventMove(Vector2 vector2)
    {
        _moveStick.onStickValueUpdated -= UpdateEventMove;
        _shopButton.onShopClicked += UpdateEventBuy;
        UpdateEvent();
    }

    public void UpdateEventBuy()
    {
        _shopButton.onShopClicked -= UpdateEventBuy;
        _shootStick.onStickTaped += UpdateEventChange;
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
