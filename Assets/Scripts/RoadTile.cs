using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : MonoBehaviour
{
    [SerializeField] Transform visual;
    public Vector2Int coordinates;

    public void Create(Vector2Int coordinates)
    {
        this.coordinates = coordinates;
        visual.localScale = Vector3.one * 0.01f;
        visual.DOScale(Vector3.one, 0.3f);
    }

    public void Death()
    {
        visual.DOScale(Vector3.one * 0.01f, 0.3f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
