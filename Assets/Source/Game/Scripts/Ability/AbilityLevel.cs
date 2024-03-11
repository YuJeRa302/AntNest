using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Level Value", menuName = "Create Ability Level", order = 51)]
public class AbilityLevel : ScriptableObject
{
    public int Level;
    public int AbilityValue;
    public int Delay;
}