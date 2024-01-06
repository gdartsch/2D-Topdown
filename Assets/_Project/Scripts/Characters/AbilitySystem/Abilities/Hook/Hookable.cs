using UnityEngine;

public class Hookable : MonoBehaviour, IHookable
{
    public void Hook(IHookable hookable, Transform hooked)
    {
        hooked.SetParent(transform);
    }

    public void Unhook(Transform hooked)
    {
        hooked.DetachChildren();
    }
}
