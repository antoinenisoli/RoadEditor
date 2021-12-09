using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : MonoBehaviour
{
    [SerializeField]public Transform visual;
    public Vector2Int coordinates;
    MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
    }

    public void Create(Vector2Int coordinates)
    {
        this.coordinates = coordinates;
        visual.localScale = Vector3.one * 0.01f;
        visual.DOScale(Vector3.one, 0.3f);
    }

    public void SetMesh(Mesh newMesh)
    {
        meshFilter.mesh = newMesh;
    }

    public void Death()
    {
        visual.DOScale(Vector3.one * 0.01f, 0.3f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
