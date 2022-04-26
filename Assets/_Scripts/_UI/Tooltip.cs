using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace BloodPeaksStudios
{
    [ExecuteInEditMode()]
    public class Tooltip : MonoBehaviour
    {
        public TextMeshProUGUI headerField; //Card Name Text
        public TextMeshProUGUI contentField; //Card Cost Text

        //Creature Card
        public TextMeshProUGUI ATKText;
        public TextMeshProUGUI HPText;
        

        public TextMeshProUGUI cardTextObject;


        public LayoutElement layoutElement;

        public int characterWrapLimit;

        public RectTransform rectTransform;

     


        public void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }


        public void SetText(string content, string header = "", string atkText = "", string hpText = "", string cardText = "")
        {
            if (string.IsNullOrEmpty(header))
            {
                headerField.gameObject.SetActive(false);
            }
            else
            {
                headerField.gameObject.SetActive(true);
                headerField.text = header;
            }

            contentField.text = content;

            ATKText.text = atkText;
            HPText.text = hpText;
            cardTextObject.text = cardText;
           

            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }

        public void Update()
        {
            Vector2 position = Input.mousePosition;

            float pivotX = position.x / Screen.width;
            float pivotY = position.y / Screen.height;

            rectTransform.pivot = new Vector2(pivotX, pivotY - 0.5f);
            transform.position = position;
        }

    }
}
