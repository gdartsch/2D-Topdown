using UnityEngine;

public interface IHookable
{
    void Hook(IHookable hookable, Transform hooker);
    void Unhook(Transform hooked);
}