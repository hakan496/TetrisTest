using System;
using System.Collections.Generic;
using UnityEngine;

namespace _GameData.Scripts
{
    public class HighlightManager : MonoBehaviour
    {
        public List<HighlightData> highlightDatas;

        public static Action<GridUnit, GridUnit, ColorsType> OnHighlightGrids;
        public static Action<GridUnit, GridUnit> OnResetHighlightGrids;

        private void OnEnable()
        {
            OnHighlightGrids += OnHighlightGridsHandler;
            OnResetHighlightGrids += OnResetHighlightGridsHandler;
        }

        private void OnHighlightGridsHandler(GridUnit firstGrid, GridUnit secondGrid, ColorsType colorsType)
        {
            var betweenGrids = GridUnitsController.FindGridsBetween(firstGrid, secondGrid);

            foreach (var gridUnit in betweenGrids)
            {
                gridUnit.SetMaterial(GetHighlightDataByColorType(colorsType).highlightMaterial);
            }
        }
        
        private void OnResetHighlightGridsHandler(GridUnit firstGrid,GridUnit secondGrid)
        {
            var betweenGrids = GridUnitsController.FindGridsBetween(firstGrid, secondGrid);

            foreach (var gridUnit in betweenGrids)
            {
                gridUnit.ResetMaterial();
            }
        }

        private HighlightData GetHighlightDataByColorType(ColorsType colorsType)
        {
            foreach (var highlightData in highlightDatas)
            {
                if (colorsType == highlightData.colorsType)
                {
                    return highlightData;
                }
            }

            Debug.LogError("Cant Find Data ");
            return new HighlightData();
        }

        private void OnDestroy()
        {
            OnHighlightGrids -= OnHighlightGridsHandler;
            OnResetHighlightGrids -= OnResetHighlightGridsHandler;
        }
    }

    [Serializable]
    public struct HighlightData
    {
        public Material highlightMaterial;
        public ColorsType colorsType;
    }
}
