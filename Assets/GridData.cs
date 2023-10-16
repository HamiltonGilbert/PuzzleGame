using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class GridData : ScriptableObject
{
    public GameObject tilePrefab;
    public GridRules gridRules;
    public RuleName[] ruleNames = new RuleName[0];
    public int rows;
    public int columns;
}
