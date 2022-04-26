using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BloodPeaksStudios
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string content;
        public string header;

        public string ATKText;
        public string HPText;
       

        public string cardText;


        CardData cardData;


        public void OnPointerEnter(PointerEventData eventData)
        {
            
            cardData = this.gameObject.transform.parent.GetComponent<SetCardData>().cardData;
            header = cardData.cardName;
            content = "Cost: " + cardData.cardCost.ToString();
            
            if (cardData.HP != 0)
            {
                HPText = "HP : " + cardData.HP.ToString();
            }
            else
            {
                HPText = "";
            }


            if (cardData.AtkDmg != 0)
            {
                ATKText = "ATK : " + cardData.AtkDmg.ToString();
            }
            else
            {
                ATKText = "";
            }

            cardText = cardData.cardText;
            

            TooltipSystem.Show(content, header, ATKText, HPText, cardText);

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.Hide();
        }

        
    }
}
