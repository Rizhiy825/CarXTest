using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CarPartsContainer : MonoBehaviour
    {
        public List<CarPartElement> Elements { get; } = new();
        
        public void Init(List<CarPartElement> carParts)
        {
            foreach (var carElement in carParts)
            {
                Elements.Add(carElement);
                carElement.transform.SetParent(transform);
            }
        }
    }
}