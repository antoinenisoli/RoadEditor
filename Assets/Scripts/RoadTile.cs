using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : MonoBehaviour
{
    [SerializeField] Transform visual;

    public void Create()
    {
        visual.localScale = Vector3.one * 0.01f;
        visual.DOScale(Vector3.one, 0.3f);
    }
}
