using UnityEngine;
using UnityEditor;


public class CardModData : ScriptableObject
{
    public string keywordName;
    public string keywordDescription;
    public enum ElementType
    {
        Fire, Water, Earth, Psychic, Light, Dark
    };
    public ElementType elementType;
    public int poisonToStack;
    public int bleedToStack;
    public bool quickAttack;
    public bool quickPlay;
    public bool overpower;
    public bool leech;
    public bool revive;

}
