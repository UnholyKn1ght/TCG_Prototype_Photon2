using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodPeaksStudios
{
    public class TooltipSystem : MonoBehaviour
    {

        private static TooltipSystem Instance;
        public Tooltip tooltip;

        public void Awake()
        {
            Instance = this;
        }

        public static void Show(string content, string header = "", string atkText = "", string hpText = "", string cardText = "")
        {
            Instance.tooltip.SetText(content, header, atkText,hpText,cardText);
            Instance.tooltip.gameObject.SetActive(true);
        }

        public static void Hide()
        {
            Instance.tooltip.gameObject.SetActive(false);
        }
    }
}