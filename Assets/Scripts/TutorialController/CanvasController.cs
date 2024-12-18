using TMPro;
using UnityEngine;

public class CanvasControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string[] _tutorialTexts;
    [SerializeField] private GameObject[] _tutorialArrows;
    [SerializeField] private TutorialController _tutorialController;

    private GameObject _currentArrow = null;

    private Vector3 _arrowOriginalScale = new Vector3(2f, 2f, 2f);
    private Vector3 _arrowAddScale = new Vector3(0.25f, 0.25f, 0.25f);
    private Vector3 _arrowUpdatedScale = new Vector3(2.5f, 2.5f, 2.5f);

    private bool _canGrow = true;

    void Start()
    {
        LevelManager.Instance.onLevelFinished += CloseTutorial;
        _tutorialController.onOpenCanvas += UpdateText;
        _tutorialController.onOpenCanvas += UpdateArrow;
    }

    private void Update()
    {
        MoveArrow();
    }

    public void CloseTutorial()
    {
        LevelManager.Instance.onLevelFinished -= CloseTutorial;
        _tutorialController.onOpenCanvas -= UpdateText;
        _tutorialController.onOpenCanvas -= UpdateArrow;

        _text.text = "";
        gameObject.SetActive(false);
    }

    private void UpdateText(int index)
    {
        if (index < _tutorialTexts.Length) _text.text = _tutorialTexts[index];
    }

    private void UpdateArrow(int index)
    {
        if (index <= _tutorialArrows.Length)
        {
            int i = index;

            foreach (var arrow in _tutorialArrows)
                arrow.SetActive(false);

            if (index == _tutorialArrows.Length) i--;

            _currentArrow = _tutorialArrows[i];
            _currentArrow.SetActive(true);
        }
        else _currentArrow.SetActive(false);
    }

    private void MoveArrow()
    {
        if (_canGrow && _currentArrow.transform.localScale.x < _arrowUpdatedScale.x && _currentArrow.transform.localScale.y < _arrowUpdatedScale.y)
            _currentArrow.transform.localScale += _arrowAddScale * Time.deltaTime;
        else _canGrow = false;

        if (!_canGrow && _currentArrow.transform.localScale.x >= _arrowOriginalScale.x && _currentArrow.transform.localScale.y >= _arrowOriginalScale.y)
            _currentArrow.transform.localScale -= _arrowAddScale * Time.deltaTime;
        else _canGrow = true;
    }
}
