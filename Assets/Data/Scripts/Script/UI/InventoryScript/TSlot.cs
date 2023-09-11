using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 orgPos;

    public Item item;
    public int itemCount;

    [Header("바인딩")]
    public Image itemImage;
    [SerializeField] private Text text_Count;
    [SerializeField] private GameObject Go_CountImage;

    void Start()
    {
        orgPos = transform.position;
    }

    //아이템 아이콘의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    //아이템 획득
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.Icon;

        if (item.Type != Item.ItemType.Equip)
        {
            Go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            Go_CountImage.SetActive(false);
        }

        SetColor(1); // 아이템이 들어왔으면 아이콘 알파값을 1로 바꾸어 활성화
    }

    //아이템 갯수
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 모든 값을 비움
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        Go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                if(item.Type == Item.ItemType.Equip)
                {
                    // 도구 장착
                }
                else
                {
                    Debug.Log(item.Name + "을 사용했습니다");
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            
            TDragSlot.Instance.dragSlot = this;
            TDragSlot.Instance.DragSetImage(itemImage);

            TDragSlot.Instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            TDragSlot.Instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (TDragSlot.Instance.dragSlot != null)
        {
            print("Call OnEndDrag");
            TDragSlot.Instance.SetColor(0);
            TDragSlot.Instance.dragSlot = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("Call OnDrop");
        ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(TDragSlot.Instance.dragSlot.item, TDragSlot.Instance.dragSlot.itemCount);

        if (_tempItem != null)
            TDragSlot.Instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else TDragSlot.Instance.dragSlot.ClearSlot();
    }

}
