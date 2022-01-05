using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float growAmount = 1.15f;
    [SerializeField] float duration = 0.15f;
    Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(baseScale * growAmount, duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(baseScale, duration);
    }
}
