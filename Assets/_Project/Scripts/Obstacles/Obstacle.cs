using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected ObstacleType type;

    public ObstacleType Type { get => type; set => type = value; }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
