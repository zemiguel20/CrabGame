using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
public class BecameInvisibleEvent : MonoBehaviour
{
    public UnityEvent invisibleEvent;

    private void OnBecameInvisible()
    {
        invisibleEvent.Invoke();
    }
}
