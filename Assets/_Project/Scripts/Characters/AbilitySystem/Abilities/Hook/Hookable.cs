using UnityEngine;

public class Hookable : MonoBehaviour, IHookable
{
    public void Hook(IHookable hookable, Transform hooker)
    {
        Debug.Log($"{hookable} hooked {hooker}");
        transform.SetParent(hooker);
    }

    public void Unhook(Transform hooker)
    {
        hooker?.DetachChildren();
    }
}
