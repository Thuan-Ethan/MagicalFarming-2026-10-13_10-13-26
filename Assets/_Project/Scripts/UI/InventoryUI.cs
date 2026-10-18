
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<SlotUI> slots = new List<SlotUI>();

    private void Awake()
    {
        inventoryPanel.SetActive(false);
    }

    private void Start()
    {
        player.inventory.OnChanged += SetupProduct;

        // Đồng bộ UI lần đầu.
        SetupProduct();
    }

    private void OnDestroy()
    {
        if (player != null && player.inventory != null)
        {
            player.inventory.OnChanged -= SetupProduct;
        }
    }

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        bool isOpening = !inventoryPanel.activeSelf;

        inventoryPanel.SetActive(isOpening);

        if (isOpening)
        {
            SetupProduct();
        }
    }

    public void SetupProduct()
    {
        if (player == null || player.inventory == null)
            return;

        if (slots.Count != player.inventory.slots.Count)
        {
            Debug.LogWarning(
                "Số lượng UI slots không khớp Inventory slots!"
            );
            return;
        }

        for (int i = 0; i < slots.Count; i++)
        {
            Inventory.Slot slot = player.inventory.slots[i];

            if (slot.type != CollectableType.NONE)
            {
                slots[i].SetItem(slot);
            }
            else
            {
                slots[i].SetEmpty();
            }
        }
    }
}
