using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rules", menuName = "GridRules", order = 1)]
public class GridRules : ScriptableObject
{
    [SerializeField] private Action[] rules;
}
