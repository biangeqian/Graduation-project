using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FPS/ItemData")]
public class ItemData_SO : ScriptableObject
{
    public string itemName;
    public string descriptionName;
    [TextArea]
    public string descriptionTooltip;
    public enum itemType{other,ammo,mag,medicine,food,gun,smallgun,knife,helmet};
    public itemType itemtype;
    public bool isStackable;
    public bool isRotatable;
    public int maxStackSize;
    public int maxValue;
    public int maxUse;//最大使用次数
    public float weightOfOne;
    public Sprite image;
    public int boxSize;
}
