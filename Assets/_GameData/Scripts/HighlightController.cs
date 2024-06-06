using System;
using System.Collections.Generic;
using UnityEngine;

namespace _GameData.Scripts
{
    public class HighlightController : MonoBehaviour
    {
        public List<Vector3> willBeCheckedDirections;
        public LayerMask blockLayer;
        public LayerMask gridLayer;

        private BlocksParent _blocksParent;
        private GridUnit _findGridUnit;
        private GridUnit _firstHighLightGridUnit;
        private GridUnit _secondHighLightGridUnit;
        private Block _block;
        
        private bool _isItSameHoldingBlockParent;
        private bool _isFirstHighlightGridFind;

        private void OnEnable()
        {
            InputManager.OnHoldingBlock += OnHoldingBlockHandler;
            InputManager.OnDropBlock += OnDropBlockHandler;
        }

        public void Awake()
        {
            _blocksParent = transform.parent.GetComponent<BlocksParent>();
            _block = GetComponent<Block>();
        }

        private void Start()
        {
            FindEmptyDirection();
        }

        private void FindEmptyDirection()
        {
            var firstIndex = willBeCheckedDirections.Count - 1;
            for (int i = firstIndex;  i > -1 ; i--)
            {
                if (Physics.Raycast(transform.position, willBeCheckedDirections[i], out var hitInfo, 1, blockLayer))
                {
                    willBeCheckedDirections.Remove(willBeCheckedDirections[i]);
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
            if (blocksParent == _blocksParent)
            {
                if(_isFirstHighlightGridFind) HighlightManager.OnResetHighlightGrids.Invoke(_firstHighLightGridUnit,_secondHighLightGridUnit);
                _isItSameHoldingBlockParent = false;
            }
        }

        public void Update()
        {
            if (_isItSameHoldingBlockParent)
            {
                if (Physics.Raycast(transform.position, Vector3.forward, out var gridHitInfo, 50, gridLayer) && gridHitInfo.transform.TryGetComponent(out GridUnit gridUnit))
                {
                    _findGridUnit = gridUnit;
                    if (_isFirstHighlightGridFind && _firstHighLightGridUnit != gridUnit)
                    {
                        HighlightManager.OnResetHighlightGrids.Invoke(_firstHighLightGridUnit,_secondHighLightGridUnit);
                    }
                    foreach (var direction in willBeCheckedDirections)
                    {
                        if (Physics.Raycast(gridUnit.transform.position, direction, out var blockHitInfo, 50, blockLayer) && blockHitInfo.transform.TryGetComponent(out Block block) )
                        {
                            if (block.ColorsType == _block.ColorsType)
                            {
                                if (_firstHighLightGridUnit == null)
                                {
                                    _isFirstHighlightGridFind = true;
                                    _firstHighLightGridUnit = gridUnit;
                                    _secondHighLightGridUnit = block.CurrentGridUnit;
                                    HighlightManager.OnHighlightGrids.Invoke(_firstHighLightGridUnit,block.CurrentGridUnit,block.ColorsType);
                                }
                                else if(_firstHighLightGridUnit != gridUnit || _findGridUnit == _firstHighLightGridUnit)
                                {
                                    print("erere");
                                    HighlightManager.OnResetHighlightGrids.Invoke(_firstHighLightGridUnit,_secondHighLightGridUnit);
                                    _firstHighLightGridUnit = gridUnit;
                                    _secondHighLightGridUnit = block.CurrentGridUnit;
                                    HighlightManager.OnHighlightGrids.Invoke(_firstHighLightGridUnit,block.CurrentGridUnit,block.ColorsType);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnDestroy()
        {
            InputManager.OnHoldingBlock -= OnHoldingBlockHandler;
            InputManager.OnDropBlock -= OnDropBlockHandler;
        }
    
    }
}
