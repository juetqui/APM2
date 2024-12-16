using UnityEngine;

public class TutorialController : MonoBehaviour, ITriggerable
{   
    private int _index = 0;
    
    public delegate void OnOpenCanvas(int canvasIndex);
    public event OnOpenCanvas onOpenCanvas = default;

    void Start()
    {
        onOpenCanvas?.Invoke(_index);
    }

    public void UpdateEvent()
    {
        _index++;
        onOpenCanvas?.Invoke(_index);
    }
}
