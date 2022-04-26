using UnityEngine;



public class CardData : ScriptableObject
{
    
    [Header("Creature Card Info")]
    [Tooltip("Name Of The Card.")]
    public string cardName;
    [Min(0)]
    public int HP;
    public int maxHP;
    [Min(0)]
    public int AtkDmg;
    [Min(0), Tooltip("How Much The Card Should Cost.")]
    public int cardCost;
    [Multiline(4), Tooltip("Description Of The Card")]
    public string cardText;
    public bool quickPlay;
    public enum ElementType { Fire,Water,Earth,Psychic,Light,Dark};
    public ElementType elementType;
    public enum CardType { Creature,Spell,Equipment}
    public CardType cardType;


    [Space(10f)]
    [Header("Card Visual Info")]
    public Sprite cardImage;
    public Sprite elementIcon;


    
    [Header("Card Mods")]
    public CardModData cardModData1;
    public CardModData cardModData2;


    [Header("Equipment Card Info")]
    [Space(25f)]
    public string eqiupmentText;
    public int increaseMaxHealth;
    public int healthHealed;// Not Used
    public int increaseDamage;
    public CardModData keyword1;
    public CardModData keyword2;

    [Header("Spell Card Info")]
    [Space(10f)]
    public string spellText;
    public int burnDamage;
    public int healthToHeal;
    public int cardsToDiscard;
    public int cardsToDraw;
    public CardData tokenToSpawn;

    [Space(15f)]
    public bool inDeck;


    public int cardDataIndex;




}
