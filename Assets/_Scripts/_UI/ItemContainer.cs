using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BloodPeaksStudios
{
    public class ItemContainer : MonoBehaviour, IDropHandler
    {
        public GameObject deckZone;
        public GameObject collectionZone;

        public enum DropZone { Collection,Deck,Nothing}
        public DropZone dropZone;



        public void OnDrop(PointerEventData eventData)
        {
            
            if (eventData.pointerDrag != null)
            {
                if (dropZone == DropZone.Collection)
                {
                    eventData.pointerDrag.gameObject.transform.SetParent(collectionZone.transform);
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = collectionZone.GetComponent<RectTransform>().anchoredPosition;
                    DeckBuilderManage.Instance.AddCardToCollection();
                    eventData = null;
                    
                }

                if (dropZone == DropZone.Deck)
                {
                    eventData.pointerDrag.gameObject.transform.SetParent(deckZone.transform);
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = deckZone.GetComponent<RectTransform>().anchoredPosition;
                    DeckBuilderManage.Instance.AddCardToDeck();
                    eventData = null;

                }

                if (dropZone == DropZone.Nothing)
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    DeckBuilderManage.Instance.DisplayCards();
                    eventData = null;
                }


                collectionZone.transform.SetAsLastSibling();
                deckZone.transform.SetAsLastSibling();
            }
            else
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = collectionZone.GetComponent<RectTransform>().anchoredPosition;
                DeckBuilderManage.Instance.DisplayCards();
                eventData = null;

            }
        }
    }
}
