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

        private List<Vector3> _emptyDirections;
        private BlocksParent _blocksParent;
        private GridUnit _firstHighLightGridUnit;
        private GridUnit _secondHighLightGridUnit;
        private Block _block;
        
        private bool _isItSameHoldingBlockParent;

        private void OnEnable()
        {
            InputManager.OnHoldingBlock += OnHoldingBlockHandler;
        }

        public void Awake()
        {
            _blocksParent = transform.parent.GetComponent<BlocksParent>();
            _block = GetComponent<Block>();
            _emptyDirections = new List<Vector3>();
           
        }

        private void Start()
        {
            FindEmptyDirection();
        }

        private void FindEmptyDirection()
        {
            foreach (var checkedDirection in willBeCheckedDirections)
            {
                if (Physics.Raycast(transform.position, checkedDirection, out var hitInfo, 1, blockLayer))
                {
                    _emptyDirections.Add(checkedDirection);
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

        public void Update()
        {
            return;
            if (_isItSameHoldingBlockParent)
            {
                if (Physics.Raycast(transform.position, Vector3.forward, out var gridHitInfo, 50, gridLayer) && gridHitInfo.transform.TryGetComponent(out GridUnit gridUnit))
                {
                    print("1");
                    foreach (var emptyDirection in _emptyDirections)
                    {
                        Debug.DrawRay(gridUnit.transform.position,emptyDirection * 100);
                        if (Physics.Raycast(gridUnit.transform.position, emptyDirection, out var blockHitInfo, 50, blockLayer) && blockHitInfo.transform.TryGetComponent(out Block block) )
                        {
                            print("2");
                            if (block.ColorsType == _block.ColorsType)
                            {
                                print("3");
                                if (_firstHighLightGridUnit == null)
                                {
                                    print("4");
                                    _firstHighLightGridUnit = gridUnit;
                                    _secondHighLightGridUnit = block.CurrentGridUnit;
                                    HighlightManager.OnHighlightGrids.Invoke(_firstHighLightGridUnit,block.CurrentGridUnit,block.ColorsType);
                                }
                                else if(_firstHighLightGridUnit != gridUnit)
                                {
                                    print("5");
                                    HighlightManager.OnResetHighlightGrids.Invoke(_firstHighLightGridUnit,block.CurrentGridUnit);
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
        }
    
    }
}
