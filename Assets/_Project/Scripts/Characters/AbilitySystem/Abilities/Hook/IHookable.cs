using UnityEngine;

public interface IHookable
{
    void Hook(IHookable hookable, Transform hooked);
    void Unhook(Transform hooked);
}