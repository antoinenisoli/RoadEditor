using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RoadTileButton : MonoBehaviour
{
    public GameObject roadPrefab;
    Image mainImage;

    private void Start()
    {
        EventManager.Instance.onButtonSelection.AddListener(SetRoadTile);
    }

    public void Init()
    {
        mainImage = GetComponent<Image>();
        Texture2D texture = AssetPreview.GetAssetPreview(roadPrefab.gameObject);
        if (texture)
        {
            Sprite spr = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), transform.position);
            mainImage.sprite = spr;
        }
    }

    public void SelectMe()
    {
        EventManager.Instance.onButtonSelection.Invoke(roadPrefab.GetComponentInChildren<MeshFilter>().sharedMesh);
    }

    public void SetRoadTile(Mesh g)
    {
        GridManager.Instance.SetTile(g);
    }
}
