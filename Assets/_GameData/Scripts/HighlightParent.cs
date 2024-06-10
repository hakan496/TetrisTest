using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _GameData.Scripts
{
    public class HighlightParent : MonoBehaviour
    {
        public HighlightController[] highlightControllers;
        private bool _isItSameHoldingBlockParent;
        private BlocksParent _blocksParent;

        private void Awake()
        {
            highlightControllers = GetComponentsInChildren<HighlightController>();
            _blocksParent = GetComponent<BlocksParent>();
        }
        private void OnEnable()
        {
            InputManager.OnHoldingBlock += OnHoldingBlockHandler;
            InputManager.OnDropBlock += OnDropBlockHandler;
        }

        public void Update()
        {
            if(!_isItSameHoldingBlockParent) return;
            
            foreach (var highlightController in highlightControllers)
            {
                var gridUnit = highlightController.FindGrid();
                if (gridUnit != null && !gridUnit.isFull)
                {
                    highlightController.Highlight(gridUnit);
                }
                else
                {
                    HighlightManager.OnResetHighlightGrids.Invoke();
                    break;
                }
            }
        }
        
                
        private void OnHoldingBlockHandler(BlocksParent blocksParent)
        {
            if (blocksParent == _blocksParent)
            {
                _isItSameHoldingBlockParent = true;
            }
        }
        
        private void OnDropBlockHandler(BlocksParent blocksParent)
        {
           
        }

        
        private void OnDestroy()
        {
            InputManager.OnHoldingBlock -= OnHoldingBlockHandler;
            InputManager.OnDropBlock -= OnDropBlockHandler;
        }
    }
}
