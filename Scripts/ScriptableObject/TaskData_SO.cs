using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Task",menuName = "Task/Task Data")]
public class TaskData_SO : ScriptableObject
{
    public int killFashiNumber;
    public Reward reward1;
    public Reward reward2;
}
[System.Serializable]
public class Reward
{
    public int money;
    public List<Item> items;
}
