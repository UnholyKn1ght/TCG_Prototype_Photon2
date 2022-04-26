using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace BloodPeaksStudios
{
    public class SetCardData : MonoBehaviour,IPointerDownHandler
    {
        
        

        public CardData cardData;


        public Image cardImage;
        public TextMeshProUGUI cardHPText;
        public TextMeshProUGUI cardDamageText;
        public TextMeshProUGUI cardCost;

      

        public TextMeshProUGUI cardModText1;
        public TextMeshProUGUI cardModText2;

        //Creature Icons
        public GameObject hpObject;
        public GameObject attackObject;

        //Spell Icons
        public GameObject spellIcons;
        public GameObject spellText;

        //Equipment Icon
        public GameObject equipmentIcon;
        public GameObject equipmentText;

        public bool isPlayed = false;
        public bool inDeck = false;

        public Button cardButton;


        public int placeNumber;

        public enum CardType { Creature, Spell, Equipment }
        public CardType setCardType;

        public UnityEvent onRightClick;
        public UnityEvent onLeftClick;

        public bool placed;
        public bool attacked;


        [Space]
        [Header("Card Stats")]
        public int cardHealth;
        public int cardDmg;
        public int cardEnergyCost;
        



        public void Start()
        {


            onLeftClick.AddListener(SetSelectedCard);
            onRightClick.AddListener(ShowCardImageWindow);
            cardImage.sprite = cardData.cardImage;
            cardCost.text = cardData.cardCost.ToString();
            SetData();
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            placed = false;
            attacked = false;

        }


        public void TakeCardDamage(int dmg)
        {
            cardData.HP -= dmg;

            if (cardData.HP <= 0)
            {
                
                Destroy(this.gameObject);
                GameManager.Instance.UpdateFieldsOnDestory();
            }
        }

        public void DestoryCard()
        {
            Destroy(this.gameObject);
            GameManager.Instance.UpdateFieldsOnDestory();
        }




        public void ShowCardImageWindow()
        {
            Debug.Log("Right Click Has Been Pressed");
        }


        private void SetData()
        {

            cardHealth = cardData.HP;
            cardDmg = cardData.AtkDmg;
            cardEnergyCost = cardData.cardCost;


            if (cardData.cardModData1 != null)
            {
                cardModText1.text = cardData.cardModData1.keywordName;
            }
            if (cardData.cardModData2 != null)
            {
                cardModText2.text = cardData.cardModData2.keywordName;
            }

            if (cardData.cardType == CardData.CardType.Creature)
            {
                setCardType = CardType.Creature;
                cardHPText.text = cardData.HP.ToString();
                cardDamageText.text = cardData.AtkDmg.ToString();
            }
            if (cardData.cardType == CardData.CardType.Equipment)
            {
                setCardType = CardType.Equipment;
                equipmentText.GetComponent<TextMeshProUGUI>().text = cardData.eqiupmentText;
            }
            if (cardData.cardType == CardData.CardType.Spell)
            {
                setCardType = CardType.Spell;
                spellText.GetComponent<TextMeshProUGUI>().text = cardData.spellText;
            }

            switch (setCardType)
            {
                case CardType.Creature:
                    hpObject.SetActive(true);
                    attackObject.SetActive(true);
                    spellIcons.SetActive(false);
                    equipmentIcon.SetActive(false);
                    spellText.SetActive(false);
                    equipmentText.SetActive(false);

                    break;
                case CardType.Equipment:
                    hpObject.SetActive(false);
                    attackObject.SetActive(false);
                    spellIcons.SetActive(false);
                    equipmentIcon.SetActive(true);
                    spellText.SetActive(false);
                    equipmentText.SetActive(true);
                    cardModText1.text = "";
                    cardModText2.text = "";
                    break;
                case CardType.Spell:
                    hpObject.SetActive(false);
                    attackObject.SetActive(false);
                    spellIcons.SetActive(true);
                    equipmentIcon.SetActive(false);
                    spellText.SetActive(true);
                    equipmentText.SetActive(false);
                    cardModText1.text = "";
                    cardModText2.text = "";
                    break;
            }
        }

        public void SetSelectedCard() 
        {
            PlayerDataManager.Instance.currentSelectedCard = cardData;
            DeckBuilderManage.Instance.selectedCard = placeNumber;

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onRightClick.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Left)
            {
                onLeftClick.Invoke();
            }
        }

        public void OnCardButtonClick()
        {
            if (GameManager.Instance.attacking == true && GameManager.Instance.playerObjects[0].GetComponent<PlayerManager>().myTurn == true && attacked == false)
            {
                GameManager.Instance.playerObjects[0].GetComponent<PlayerManager>().attackingCard = this;
               
            }

            if (GameManager.Instance.attacking == false && GameManager.Instance.playerObjects[0].GetComponent<PlayerManager>().myTurn == false)
            {
                GameManager.Instance.playerObjects[0].GetComponent<PlayerManager>().defendingCard = this;

            }

        }


    }
}
