using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BloodPeaksStudios
{
    public class ElementIconStorage : MonoBehaviour
    {
        public static ElementIconStorage Instance;

        CardData cardData;
        Image elementIcon;

        public List<Sprite> elementIcons = new List<Sprite>();

        public void Awake()
        {
            Instance = this;
        }


        public void Update()
        {

            cardData = this.transform.parent.GetComponent<SetCardData>().cardData;
            elementIcon = this.GetComponent<Image>();

            switch (cardData.elementType)
            {
                case CardData.ElementType.Dark:
                    elementIcon.sprite = elementIcons[0];
                    break;
                case CardData.ElementType.Earth:
                    elementIcon.sprite = elementIcons[1];
                    break;
                case CardData.ElementType.Fire:
                    elementIcon.sprite = elementIcons[2];
                    break;
                case CardData.ElementType.Light:
                    elementIcon.sprite = elementIcons[3];
                    break;
                case CardData.ElementType.Psychic:
                    elementIcon.sprite = elementIcons[4];
                    break;
                case CardData.ElementType.Water:
                    elementIcon.sprite = elementIcons[5];
                    break;


            }

        }
    }


}



