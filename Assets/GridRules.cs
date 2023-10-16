using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class GridRules: MonoBehaviour
{
    [SerializeField] string[] ruleNames = new string[0];
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
