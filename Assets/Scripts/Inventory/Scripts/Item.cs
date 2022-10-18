﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{

    public enum ItemType
    {
        Sword,
        Wood,
        Slime,
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.Wood: return ItemAssets.Instance.woodSprite;
            case ItemType.Slime: return ItemAssets.Instance.slimeSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
                return true;
            case ItemType.Sword:
                return false;
        }
    }

}
