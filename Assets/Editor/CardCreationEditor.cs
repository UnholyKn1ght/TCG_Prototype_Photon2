using UnityEngine;
using UnityEditor;
using System;

public enum ElementType 
{ 
    Fire, Water, Earth, Psychic, Light, Dark
};

public enum CardType
{
    Creature, Spell, Equipment
};

namespace BloodPeaksStudios
{
    public class CardCreationEditor : EditorWindow
    {
        //Enums
        public ElementType elementType;
        public CardType cardType;

        //Creature Card Data
        string cardName;
        string cardDescription;
        Sprite cardImage;
        int cardCost;
        int cardHP;
        int cardDMG;
        CardModData cardMod1;
        CardModData cardMod2;

        //Equipment Card Data

        int increaseMaxHealth;
        int healthHealed;
        int increaseDamage;
        CardModData keyword1;
        CardModData keyword2;
        string eqiupmentText;


        //Spell Card Data

        int burnDamage;
        int healthToHeal;
        int cardsToDiscard;
        int cardsToDraw;
        CardData tokenToSpawn;
        string spellText;

        //Keyword Card Data

        string keywordName;
        string keywordDescription;
        int poisonToStack;
        int bleedToStack;
        bool quickAttack;
        bool quickPlay;
        bool overpower;
        bool leech;
        bool revive;



        //Tab Management
        string[] toolbarStrings = { "Creature", "Equipment", "Spell", "Keywords" };
        int _toolbar_sel = 0;
        string cardStat = "";


        [MenuItem("Card Master/ Card Creation")]
        public static void ShowWindow()
        {
            GetWindow<CardCreationEditor>("Card Creation");
        }


        private void OnGUI()
        {
            
            GUILayout.Label("Card Creation Window", EditorStyles.boldLabel);
            EditorGUILayout.Space(3);
            GUILayout.BeginHorizontal();
            _toolbar_sel = GUILayout.Toolbar(_toolbar_sel, toolbarStrings);
            GUILayout.EndHorizontal();

            switch (_toolbar_sel) { 
                //------------------------------------------------------------------------------------
                case 0: //Creature Cards
                    
                    EditorGUILayout.Space();
                    cardName = EditorGUILayout.TextField("Card Name", cardName);

                    //Creature Image
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Card Image");
                    cardImage = (Sprite)EditorGUILayout.ObjectField(cardImage, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    //Creature Cost

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cost");
                    cardCost = EditorGUILayout.IntField(cardCost);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Card Text");
                    cardDescription = EditorGUILayout.TextArea(cardDescription);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(15f);

                    //Creature HP
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("HP");
                    cardHP = EditorGUILayout.IntField(cardHP);
                    EditorGUILayout.EndHorizontal();

                    

                    //Creature Damage

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Damage");
                    cardDMG = EditorGUILayout.IntField(cardDMG);
                    EditorGUILayout.EndHorizontal();

                    //Creature Keyword 1 
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Keyword 1");
                    cardMod1 = (CardModData)EditorGUILayout.ObjectField(cardMod1, typeof(CardModData), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    //Creature Keyword 2
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Keyword 2");
                    cardMod2 = (CardModData)EditorGUILayout.ObjectField(cardMod2, typeof(CardModData), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    //Creature Element
                    EditorGUILayout.BeginHorizontal();
                    elementType = (ElementType)EditorGUILayout.EnumPopup("Card Element", elementType);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    //Create The Card
                    if (GUILayout.Button("Create New Creature Card"))
                    {
                        if (cardImage != null && cardName != null)
                        {
                            CreateNewCreatureCard();
                            cardStat = "Card Created";
                        }
                        else
                        {
                            cardStat = "";
                            cardStat = "Card Failed To Create (Not Enough Info)";
                        }

                    }
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField(cardStat, EditorStyles.largeLabel);

                    

                    break;

                //---------------------------------------------------------------------------------------
                case 1: //Equipment Cards
                    EditorGUILayout.Space();
                    cardName = EditorGUILayout.TextField("Card Name", cardName);

                    //Creature Image
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Card Image");
                    cardImage = (Sprite)EditorGUILayout.ObjectField(cardImage, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();


                    //Creature Cost

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cost");
                    cardCost = EditorGUILayout.IntField(cardCost);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Card Text");
                    eqiupmentText = EditorGUILayout.TextArea(eqiupmentText);
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.Space(15f);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("IncreaseMaxHealth");
                    increaseMaxHealth = EditorGUILayout.IntField(increaseMaxHealth);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Health To Heal");
                    healthHealed = EditorGUILayout.IntField(healthHealed);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Damage Increase");
                    increaseDamage = EditorGUILayout.IntField(increaseDamage);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space(10f);
                    GUILayout.Label("Keywords To Add", EditorStyles.boldLabel);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Keyword 1");
                    keyword1 = (CardModData)EditorGUILayout.ObjectField(keyword1, typeof(CardModData), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Keyword 2");
                    keyword2 = (CardModData)EditorGUILayout.ObjectField(keyword2, typeof(CardModData), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    //Creature Element
                    EditorGUILayout.BeginHorizontal();
                    elementType = (ElementType)EditorGUILayout.EnumPopup("Card Element", elementType);
                    EditorGUILayout.EndHorizontal();



                    

                    EditorGUILayout.Space();
                    //Create The Card
                    if (GUILayout.Button("Create New Eqiupment Card"))
                    {
                        if (cardImage != null && cardName != null)
                        {
                            CreateNewEquipmentCard();
                            cardStat = "Card Created";
                        }
                        else
                        {
                            cardStat = "";
                            cardStat = "Card Failed To Create (Not Enough Info)";
                        }

                    }
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField(cardStat, EditorStyles.largeLabel);
                    break;

                 //------------------------------------------------------------------------------------

                case 2: //Spell Cards
                    EditorGUILayout.Space();
                    cardName = EditorGUILayout.TextField("Card Name", cardName);

                    //Creature Image
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Card Image");
                    cardImage = (Sprite)EditorGUILayout.ObjectField(cardImage, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();


                    //Creature Cost

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cost");
                    cardCost = EditorGUILayout.IntField(cardCost);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Card Text");
                    spellText = EditorGUILayout.TextArea(spellText);
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Quick Play");
                    quickPlay = EditorGUILayout.Toggle(quickPlay);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space(15f);

                    
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Burn Damage");
                    burnDamage = EditorGUILayout.IntField(burnDamage);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Health Healed");
                    healthToHeal = EditorGUILayout.IntField(healthToHeal);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cards Discarded");
                    cardsToDiscard = EditorGUILayout.IntField(cardsToDiscard);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cards Draw");
                    cardsToDraw = EditorGUILayout.IntField(cardsToDraw);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Token To Spawn");
                    tokenToSpawn = (CardData)EditorGUILayout.ObjectField(tokenToSpawn, typeof(CardData), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    //Creature Element
                    EditorGUILayout.BeginHorizontal();
                    elementType = (ElementType)EditorGUILayout.EnumPopup("Card Element", elementType);
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.Space();
                    //Create The Card
                    if (GUILayout.Button("Create New Spell Card"))
                    {
                        if (cardImage != null && cardName != null)
                        {
                            CreateNewSpellCard();
                            cardStat = "Card Created";
                        }
                        else
                        {
                            cardStat = "";
                            cardStat = "Card Failed To Create (Not Enough Info)";
                        }

                    }
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField(cardStat, EditorStyles.largeLabel);
                    break;

                //---------------------------------------------------------------------------------

                case 3:
                    EditorGUILayout.Space();
                    keywordName = EditorGUILayout.TextField("Keyword Name", keywordName);
                    keywordDescription = EditorGUILayout.TextField("Keyword Descriptiong", keywordDescription);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Poison Stacks");
                    poisonToStack = EditorGUILayout.IntField(poisonToStack);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Bleed Stacks");
                    bleedToStack = EditorGUILayout.IntField(bleedToStack);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Quick Attack");
                    quickAttack = EditorGUILayout.Toggle(quickAttack);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Quick Play");
                    quickPlay = EditorGUILayout.Toggle(quickPlay);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Overpower");
                    overpower = EditorGUILayout.Toggle(overpower);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Leech");
                    leech = EditorGUILayout.Toggle(leech);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Revive");
                    revive = EditorGUILayout.Toggle(revive);
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.Space();
                    //Create The Card
                    if (GUILayout.Button("Create New Keyword"))
                    {
                        if (keywordName != null && keywordDescription != null)
                        {
                            CreateNewKeyword();
                            cardStat = "Card Created";
                        }
                        else
                        {
                            cardStat = "";
                            cardStat = "Card Failed To Create (Not Enough Info)";
                        }

                    }
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField(cardStat, EditorStyles.largeLabel);
                   
                    break;


            }
            
        }


        public void CreateNewKeyword()
        {
            CardModData cardKeyword = ScriptableObject.CreateInstance<CardModData>();

            cardKeyword.keywordName = keywordName;
            cardKeyword.keywordDescription = keywordDescription;
            cardKeyword.poisonToStack = poisonToStack;
            cardKeyword.bleedToStack = bleedToStack;
            cardKeyword.quickAttack = quickAttack;
            cardKeyword.quickPlay = quickPlay;
            cardKeyword.overpower = overpower;
            cardKeyword.leech = leech;
            cardKeyword.revive = revive;




            //Switch Case For Element
            switch (elementType)
            {
                case ElementType.Dark:
                    cardKeyword.elementType = CardModData.ElementType.Dark;
                    break;
                case ElementType.Earth:
                    cardKeyword.elementType = CardModData.ElementType.Earth;
                    break;
                case ElementType.Fire:
                    cardKeyword.elementType = CardModData.ElementType.Fire;
                    break;
                case ElementType.Light:
                    cardKeyword.elementType = CardModData.ElementType.Light;
                    break;
                case ElementType.Psychic:
                    cardKeyword.elementType = CardModData.ElementType.Psychic;
                    break;
                case ElementType.Water:
                    cardKeyword.elementType = CardModData.ElementType.Water;
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }



            //Saving New Card Asset
            AssetDatabase.CreateAsset(cardKeyword, "Assets/_Scripts/_Database/CardModFolder/" + keywordName + ".asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = cardKeyword;

            cardStat = "";
        }

        private void CreateNewEquipmentCard()
        {
            //Calling Card Data ScriptableObject
            CardData cardData = ScriptableObject.CreateInstance<CardData>();

            //Setting New Card Data
            cardData.cardName = cardName;
            cardData.cardImage = cardImage;
            cardData.cardCost = cardCost;
            cardData.increaseMaxHealth = increaseMaxHealth;
            cardData.healthHealed = healthHealed;
            cardData.increaseDamage = increaseDamage;
            cardData.eqiupmentText = eqiupmentText;
            cardData.keyword1 = keyword1;
            cardData.keyword2 = keyword2;



            //Switch Case For Element
            switch (elementType)
            {
                case ElementType.Dark:
                    cardData.elementType = CardData.ElementType.Dark;
                    break;
                case ElementType.Earth:
                    cardData.elementType = CardData.ElementType.Earth;
                    break;
                case ElementType.Fire:
                    cardData.elementType = CardData.ElementType.Fire;
                    break;
                case ElementType.Light:
                    cardData.elementType = CardData.ElementType.Light;
                    break;
                case ElementType.Psychic:
                    cardData.elementType = CardData.ElementType.Psychic;
                    break;
                case ElementType.Water:
                    cardData.elementType = CardData.ElementType.Water;
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }

            switch (_toolbar_sel)
            {
                case 0:
                    cardData.cardType = CardData.CardType.Creature;
                    break;
                case 1:
                    cardData.cardType = CardData.CardType.Equipment;
                    break;
                case 2:
                    cardData.cardType = CardData.CardType.Spell;
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }

            //Saving New Card Asset
            AssetDatabase.CreateAsset(cardData, "Assets/_Scripts/_Database/CardFolder/" + cardName + ".asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = cardData;

            cardStat = "";
        }

        private void CreateNewSpellCard()
        {
            //Calling Card Data ScriptableObject
            CardData cardData = ScriptableObject.CreateInstance<CardData>();

            //Setting New Card Data
            cardData.cardName = cardName;
            cardData.cardImage = cardImage;
            cardData.cardCost = cardCost;
            cardData.burnDamage = burnDamage;
            cardData.healthToHeal = healthToHeal;
            cardData.cardsToDiscard = cardsToDiscard;
            cardData.cardsToDraw = cardsToDraw;
            cardData.tokenToSpawn = tokenToSpawn;
            cardData.spellText = spellText;

            cardData.quickPlay = quickPlay;

            //Switch Case For Element
            switch (elementType)
            {
                case ElementType.Dark:
                    cardData.elementType = CardData.ElementType.Dark;
                    break;
                case ElementType.Earth:
                    cardData.elementType = CardData.ElementType.Earth;
                    break;
                case ElementType.Fire:
                    cardData.elementType = CardData.ElementType.Fire;
                    break;
                case ElementType.Light:
                    cardData.elementType = CardData.ElementType.Light;
                    break;
                case ElementType.Psychic:
                    cardData.elementType = CardData.ElementType.Psychic;
                    break;
                case ElementType.Water:
                    cardData.elementType = CardData.ElementType.Water;
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }

            switch (_toolbar_sel)
            {
                case 0:
                    cardData.cardType = CardData.CardType.Creature;
                    break;
                case 1:
                    cardData.cardType = CardData.CardType.Equipment;
                    break;
                case 2:
                    cardData.cardType = CardData.CardType.Spell;
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }

            //Saving New Card Asset
            AssetDatabase.CreateAsset(cardData, "Assets/_Scripts/_Database/CardFolder/" + cardName + ".asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = cardData;

            cardStat = "";
        }

        public void CreateNewCreatureCard()
        {
            //Calling Card Data ScriptableObject
            CardData cardData = ScriptableObject.CreateInstance<CardData>();

            //Setting New Card Data
            cardData.cardName = cardName;
            cardData.cardImage = cardImage;
            cardData.cardText = cardDescription;
            cardData.HP = cardHP;
            cardData.maxHP = cardHP;
            cardData.cardCost = cardCost;
            cardData.AtkDmg = cardDMG;
            cardData.cardModData1 = cardMod1;
            cardData.cardModData2 = cardMod2;
            
            
            //Switch Case For Element
            switch (elementType)
            {
                case ElementType.Dark:
                    cardData.elementType = CardData.ElementType.Dark;
                    break;
                case ElementType.Earth:
                    cardData.elementType = CardData.ElementType.Earth;
                    break;
                case ElementType.Fire:
                    cardData.elementType = CardData.ElementType.Fire;
                    break;
                case ElementType.Light:
                    cardData.elementType = CardData.ElementType.Light;
                    break;
                case ElementType.Psychic:
                    cardData.elementType = CardData.ElementType.Psychic;
                    break;
                case ElementType.Water:
                    cardData.elementType = CardData.ElementType.Water;
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }

            switch (_toolbar_sel)
            {
                case 0:
                    cardData.cardType = CardData.CardType.Creature;
                    break;
                case 1:
                    cardData.cardType = CardData.CardType.Equipment;
                    break;
                case 2:
                    cardData.cardType = CardData.CardType.Spell;
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }

            //Saving New Card Asset
            AssetDatabase.CreateAsset(cardData, "Assets/_Scripts/_Database/CardFolder/" + cardName + ".asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = cardData;

            cardStat = "";
        }

        
    }
}
