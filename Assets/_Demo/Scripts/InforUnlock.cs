using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InforUnlock", menuName = "ScriptableObjects/InforUnlock")]
[SerializeField]
public class InforUnlock : ScriptableObject
{
    public List<int> ListUnlockCheft = new List<int>();
    public List<int> ListUnlockTable = new List<int>();
}
