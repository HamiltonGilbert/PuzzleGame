using UnityEngine;
using static Rules;

[CreateAssetMenu(fileName = "Area", menuName = "AreaData", order = 1)]
public class AreaData : ScriptableObject
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private RuleName[] ruleNames = new RuleName[0];
    [SerializeField] private LevelData[] levelDatas;

    public LevelData GetLevel(int index) { return levelDatas[index]; }
    public int NumOfLevels() { return levelDatas.Length; }
    public RuleName[] GetRuleNames() { return ruleNames; }
    public void Initialize()
    {
        foreach (LevelData levelData in levelDatas)
        {
            levelData.SetRules(ruleNames);
            levelData.SetTile(tilePrefab);
        }
            
    }
}

