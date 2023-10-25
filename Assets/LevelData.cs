using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Rules;

[CreateAssetMenu(fileName = "Level", menuName = "LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public GameObject tilePrefab;
    public RuleName[] ruleNames = new RuleName[0];
    public int rows = 1;
    public int columns = 1;
    public Remove[] removeRanges;
    public Fixed[] fixedTiles;
    public Numbered[] numberedTiles;


    [Serializable]
    public struct Remove
    {
        public bool row;
        public int[] range;
    }
    [Serializable]
    public struct Fixed
    {
        public int row;
        public int column;
        public bool value;
    }
    [Serializable]
    public struct Numbered
    {
        public int row;
        public int column;
        public int number;
    }
}

