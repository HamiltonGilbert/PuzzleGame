using System;
using UnityEngine;
using static Rules;

[CreateAssetMenu(fileName = "Level", menuName = "LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    private GameObject tilePrefab;
    private RuleName[] ruleNames = new RuleName[0];
    public int rows = 1;
    public int columns = 1;
    public Remove[] removeRanges;
    public Fixed[] fixedTiles;
    public Numbered[] numberedTiles;

    public void SetRules(RuleName[] ruleNames) { this.ruleNames = ruleNames; }
    public RuleName[] GetRules() { return ruleNames; }
    public void SetTile(GameObject tilePrefab) { this.tilePrefab = tilePrefab; }
    public GameObject GetTile() { return tilePrefab; }

    [Serializable]
    public struct Remove
    {
        public bool row;
        public int number;
        public int start;
        public int finish;
    }
    [Serializable]
    public struct Fixed
    {
        public int row;
        public int column;
        public bool state;
    }
    [Serializable]
    public struct Numbered
    {
        public int row;
        public int column;
        public int number;
    }
}

