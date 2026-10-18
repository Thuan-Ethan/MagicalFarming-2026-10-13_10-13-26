using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SlotUI : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI quantityText;

    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            itemImage.sprite = slot.itemIcon;
            itemImage.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();
        }
    }

    public void SetEmpty()
    {
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }
}
