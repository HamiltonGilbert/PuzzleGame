using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

[CreateAssetMenu(fileName = "GridData", menuName = "GridData", order = 1)]
public class GridData : ScriptableObject
{
    public GameObject tilePrefab;
    public RuleName[] ruleNames = new RuleName[0];
    public int rows;
    public int columns;
}
