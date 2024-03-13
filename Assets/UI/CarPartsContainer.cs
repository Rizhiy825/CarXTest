using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CarPartsContainer : MonoBehaviour
    {
        private List<CarPartElement> elements = new();
        
        public void Init(List<CarPartElement> carParts)
        {
            foreach (var carElement in carParts)
            {
                elements.Add(carElement);
                carElement.transform.SetParent(transform);
            }
        }
    }
}