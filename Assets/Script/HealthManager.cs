using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    private Dictionary<string, Health> characterHealth = new Dictionary<string, Health>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddCharacter(string characterName, Health healthScript)
    {
        if (!characterHealth.ContainsKey(characterName))
        {
            characterHealth.Add(characterName, healthScript);
        }
    }

    public void IncreaseHealth(string characterName, int value)
    {
        if (characterHealth.ContainsKey(characterName))
        {
            characterHealth[characterName].IncreaseHealth(value);
        }
    }
}
