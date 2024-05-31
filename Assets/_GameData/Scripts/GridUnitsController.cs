using UnityEngine;

namespace _GameData.Scripts
{
    public class GridUnitsController 
    {
        public GridUnit[,] GridUnits { get; set; }

        public GridUnitsController(GridUnit[,] gridUnits)
        {
            GridUnits = gridUnits;
        }
    }
}
