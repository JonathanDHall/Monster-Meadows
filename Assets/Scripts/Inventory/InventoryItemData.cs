using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")] [Serializable]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
}