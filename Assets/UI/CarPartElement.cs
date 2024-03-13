using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CarPartElement : MonoBehaviour
    {
        public Button button;
        public TMP_Text text;

        public void Init(CarPartData carPartData)
        {
            text.text = carPartData.partName;
        }
    }
}