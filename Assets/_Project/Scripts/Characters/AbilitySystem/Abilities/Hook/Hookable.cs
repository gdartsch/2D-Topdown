using UnityEngine;

public class Hookable : MonoBehaviour, IHookable
{
    public void Hook(IHookable hookable, Transform hooker)
    {
        transform.SetParent(hooker);
    }

    public void Unhook(Transform hooker)
    {
        hooker?.DetachChildren();
    }
}
