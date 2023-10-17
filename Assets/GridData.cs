using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Rules;

[CreateAssetMenu(fileName = "GridData", menuName = "GridData", order = 1)]
public class GridData : ScriptableObject
{
    public GameObject tilePrefab;
    public RuleName[] ruleNames = new RuleName[0];
    public int rows = 1;
    public int columns = 1;

    [Serializable]
    public struct Fixed
    {
        public int row;
        public int column;
        public bool value;
    }
    public Fixed[] fixedTiles;
}

