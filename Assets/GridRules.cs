using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rules", menuName = "GridRules", order = 1)]
public class GridRules : ScriptableObject
{
    [SerializeField] string[] ruleNames;
    private Func<Solve, bool>[] rules;
    public void Awake()
    {
        rules = new Func<Solve, bool>[ruleNames.Length];
        for (int i = 0; i < ruleNames.Length; i++)
        {
            rules[i] = (Func<Solve, bool>) Delegate.CreateDelegate(typeof(Rules), typeof(Rules).GetMethod(ruleNames[i]));
        }
    }
}
