using UnityEngine;

public class TriggerLeaveDisable : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        gameObject.SetActive(false);
    }
}
