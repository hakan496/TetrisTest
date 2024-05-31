using UnityEngine;

namespace _GameData.Scripts
{
    public class GridUnit : MonoBehaviour
    {
        public bool isFull;

        private Block _currentBlock;
        
        public void SetGridUnit(Block block)
        {
            _currentBlock = block;
            isFull = true;
        }

        public void EmptySlot()
        {
            _currentBlock = null;
            isFull = false;
        }
    }
}
