using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHookable hookable))
        {
            hookable.Hook(hookable, transform);
        }
    }
}
