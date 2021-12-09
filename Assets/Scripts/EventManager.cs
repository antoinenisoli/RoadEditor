using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public UnityEvent onNewRoad = new UnityEvent();
    public class MeshEvent : UnityEvent<Mesh> { }

    public MeshEvent onButtonSelection = new MeshEvent();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }
}
