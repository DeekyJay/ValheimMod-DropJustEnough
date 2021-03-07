namespace DropJustEnough.GameClasses
{
    using HarmonyLib;
    using System;
    using UnityEngine;

    [HarmonyPatch(typeof(InventoryGrid), "OnLeftClick")]
    public static class InventoryGrid_Patch_OnLeftClick_DropJustEnoughItems
    {
        private static bool Prefix(ref InventoryGrid __instance, ref Inventory ___m_inventory, UIInputHandler clickHandler)
        {
            Vector2i buttonPos = __instance.GetButtonPos(clickHandler.gameObject);
            ItemDrop.ItemData itemAt = ___m_inventory.GetItemAt(buttonPos.x, buttonPos.y);

            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                return !DropJustEnoughItems(___m_inventory, itemAt);
            }

            return true;
        }

        private static bool DropJustEnoughItems(Inventory inventory, ItemDrop.ItemData item)
        {
            var totalWeight = inventory.GetTotalWeight();
            var totalItemWeight = item.GetWeight();
            var singleItemWeight = totalItemWeight / item.m_stack;
            var maxCarryWeight = Player.m_localPlayer.GetMaxCarryWeight();
            var weightToRemove = totalWeight - maxCarryWeight;
            var howManyToRemove = totalItemWeight > weightToRemove ? (int)Math.Ceiling(weightToRemove / singleItemWeight) : item.m_stack;
            return Player.m_localPlayer.DropItem(inventory, item, howManyToRemove);
        }
    }
}
