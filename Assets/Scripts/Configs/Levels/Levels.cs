using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Levels", fileName = "Levels")]
public class Levels : ScriptableObject
{
    public LevelInfo[] LevelArray;
    public int CurrentLevelIndex;
}