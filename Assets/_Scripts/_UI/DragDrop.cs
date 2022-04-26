using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;



    GameObject collectionZone;
    GameObject deckZone;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            collectionZone = GameObject.Find("CardCollectionWindow");
            deckZone = GameObject.Find("DeckWindow");
            canvasGroup = GetComponent<CanvasGroup>();
        }
        else if (SceneManager.GetActiveScene().name == "MainGameScene")
        {
            this.enabled = false;

        }

        
        

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        
       
        if (SceneManager.GetActiveScene().name == "MainMenu" && eventData.button == PointerEventData.InputButton.Left)
        {
            canvasGroup.alpha = 0.7f;
            canvasGroup.blocksRaycasts = false;
            collectionZone.transform.SetAsFirstSibling();
            deckZone.transform.SetAsFirstSibling();
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && eventData.button == PointerEventData.InputButton.Left)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && eventData.button == PointerEventData.InputButton.Left)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
        
       
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && eventData.button == PointerEventData.InputButton.Left)
        {
            this.gameObject.transform.SetParent(GameObject.Find("DeckBuilderWindow").transform);
            rectTransform.SetAsLastSibling();
        }
     


        
    }

 
}
