using UnityEngine;

namespace _GameData.Scripts
{
    public class GridUnit : MonoBehaviour
    {
        public bool isFull;

        private Block _currentBlock;

        private int _indexX;

        public int IndexX => _indexX;

        public int IndexY => _indexY;

        private int _indexY;

        [SerializeField] private MeshRenderer _meshRenderer;
        private Material _defaultMat;
        public void Init(int indexX,int indexY)
        {
            _indexX = indexX;
            _indexY = indexY;
            _defaultMat = _meshRenderer.material;

        }
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

        public void SetMaterial(Material material)
        {
            _meshRenderer.material = material;
        }

        public void ResetMaterial()
        {
            _meshRenderer.material = _defaultMat;
        }
    }
}
