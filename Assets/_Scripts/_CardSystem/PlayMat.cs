using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;


/// <summary>
/// Handles the cards being places onto the playing field from hand.
/// </summary>

namespace BloodPeaksStudios
{
    public class PlayMat : MonoBehaviour, IDropHandler
    {
        [SerializeField] bool isPlayingField; //Check To See If Card Can Be Played On Selected UI

        /// <summary>
        /// eventData : The GameObject That Is Being Held By The Mouse Down
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {

            GameObject dropObject;
            dropObject = eventData.pointerDrag.gameObject;


            if (eventData.pointerDrag != null && isPlayingField == true && GameManager.Instance.playerObjects[0].GetComponent<PlayerManager>().playerCards.Count < 5 && GameManager.Instance.playerObjects[0].GetComponent<PlayerManager>().myTurn == true)
            {

                if (eventData.pointerDrag != null && GameManager.Instance.playerObjects[0].GetComponent<PlayerManager>().energyPoints >= dropObject.GetComponent<SetCardData>().cardData.cardCost)
                {
                    
                    dropObject.transform.SetParent(this.transform);
                    dropObject.GetComponent<PlayerHandDragHandler>().canDrag = false;
                    dropObject.GetComponent<CanvasGroup>().alpha = 1f;
                    dropObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    GameManager.Instance.SendCardData(dropObject.GetComponent<SetCardData>().cardData.cardDataIndex, 0);
                    GameManager.Instance.UsePlayerEnergy(dropObject.GetComponent<SetCardData>().cardData.cardCost);
                    dropObject.transform.Find("CardButtonClick").transform.gameObject.SetActive(true);
                    dropObject = null;

                }
                else //Used When The Player Doesnt Have Enough Energy To Play(Return To Hand)
                {
                    dropObject.transform.SetParent(GameObject.Find("PlayerHand").transform);
                    dropObject.GetComponent<PlayerHandDragHandler>().canDrag = true;
                    dropObject.GetComponent<CanvasGroup>().alpha = 1f;
                    dropObject = null;

                }
            }
            
            
        }

    }

}

       

    

