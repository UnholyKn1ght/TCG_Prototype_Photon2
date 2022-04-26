using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerHandDragHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool canDrag; 


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (SceneManager.GetActiveScene().name == "OnlinePlayingField")
        {
            this.enabled = true;
            canvas = GameObject.Find("PlayCanvas").GetComponent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();

        }

        canDrag = true;
    }  

        

    


    public void OnBeginDrag(PointerEventData eventData)
    {

        if (SceneManager.GetActiveScene().name == "OnlinePlayingField" && eventData.button == PointerEventData.InputButton.Left)
        {
            if (canDrag == true)
            {
                canvasGroup.alpha = 0.7f;
                canvasGroup.blocksRaycasts = false;
            }
        }
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "OnlinePlayingField" && eventData.button == PointerEventData.InputButton.Left)
        {
            if (canDrag == true)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
                Debug.Log("OnDrag");
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "OnlinePlayingField" && eventData.button == PointerEventData.InputButton.Left)
        {
            if (canDrag == true)
            {

                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            
        }
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "OnlinePlayingField" && eventData.button == PointerEventData.InputButton.Left)
        {
            if (canDrag == true)
            {

                this.gameObject.transform.SetParent(GameObject.Find("PlayCanvas").transform);
                rectTransform.SetAsLastSibling();
            }

        }

        
    }

    
}
