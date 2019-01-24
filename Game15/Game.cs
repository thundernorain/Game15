using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game15
{
    class Game
    {
        int size;//number of cells
        int[,] cells;//matrix for game cells
        int emptyX, emptyY;//coordinats of empty cell
        static Random Rnd = new Random();//need for set random numbers in cells

        public Game(int size)
        {
            if (size < 2) size = 2;
            if (size > 7) size = 7;

            this.size = size;
            cells = new int[size, size];
        }
        public void Start()
        {
            emptyX = size - 1;
            emptyY = size - 1;

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    cells[i, j] = CoordsToPos(i, j) + 1;

            cells[emptyX, emptyY] = 0;
        }
        public bool CheckForEnd()//check for end game
        {
            if (cells[size - 1, size - 1] != 0) return false;//if last cell is not empty then game not ended
            for (int pos = 0; pos < size * size - 1; pos++)
                if (GetNumber(pos) != pos + 1) return false;//if all cells are in chaos then game not ended
            return true;//if last cell empty and all cells is sorted then game ended
        }
        public int GetNumber(int pos)//get number in cell
        {
            int x, y;
            PosToCoords(pos, out x, out y);

            if (x < 0 || x >= size) return 0;
            if (y < 0 || y >= size) return 0;

            return cells[x, y];
        }
        private int CoordsToPos(int x, int y)//get coordinats from position
        {
            if (x < 0) x = 0;
            if (x > size - 1) x = size - 1;
            if (y < 0) y = 0;
            if (y > size - 1) y = size - 1;

            return y * size + x;
        }
        private void PosToCoords(int pos, out int x, out int y)//get position from coordinats
        {
            if (pos < 0) pos = 0;
            if (pos > size * size - 1) pos = size * size - 1;

            x = pos % size;
            y = pos / size;
        }
        public void Shift(int pos)//shifting coordinats with empty cell
        {
            int x, y;
            PosToCoords(pos, out x, out y);

            if (Math.Abs(emptyX - x) + Math.Abs(emptyY - y) != 1) return;//check for neighbors with empty cell

            cells[emptyX, emptyY] = GetNumber(pos);
            cells[x, y] = 0;
            emptyX = x;
            emptyY = y;
        }
        public void ShiftRandom()//randomly sets the numbers in new game
        {
            int x = emptyX;
            int y = emptyY;

            int num = Rnd.Next(0, 4);
            switch (num)
            {
                case 0: x++;break;
                case 1: x--;break;
                case 2: y++;break;
                case 3: y--;break;
            }

            Shift(CoordsToPos(x,y));
        }
    }
}
