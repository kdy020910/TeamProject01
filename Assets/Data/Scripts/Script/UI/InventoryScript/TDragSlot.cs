using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDragSlot : TSingleton<TDragSlot>
{
    public TSlot dragSlot;

    [SerializeField] private Image ImageItem;
    
    
    private void Awake()
    {
        Init();
    }

    public void DragSetImage(Image _itemImage)
    {
        ImageItem.sprite = _itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = ImageItem.color;
        color.a = _alpha;
        ImageItem.color = color;
    }
}