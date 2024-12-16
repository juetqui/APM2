using UnityEngine;

public class TutorialChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        ITriggerable tutorialTrigger = coll.GetComponent<ITriggerable>();

        if (tutorialTrigger != null)
        {
            tutorialTrigger.UpdateEvent();
            Destroy(gameObject);
        }
    }
}
