using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoadTileButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject roadPrefab;
    Image mainImage;
    Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(baseScale * 1.15f, 0.15f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(baseScale, 0.15f);
    }
}
