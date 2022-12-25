using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing ;
namespace KTetris
{
    public class Block
    {

        private Game.enBlockType _BlockType;
        public Game.enBlockType BlockType
        {
            get { return _BlockType; }
        }
        private Color _BColor;
        public Color BColor
        {
            get { return _BColor; }
        }
        private int _Size;
        public int Size
        {
            get { return _Size; }
        }

        public GenericTable<int> RolateTableint
        {
            get
            {
                return _lstRolation[GetCurrentRolateIndex()];
            }
        }
        public GenericTable<int> Tableint
        {
            get
            {
                return _lstRolation[_CurrentIndexRolation];
            }
        }
        private  List<GenericTable<int>> _lstRolation;
        public List<GenericTable<int>> lstRolation
        {
            get { return _lstRolation; }
        }
        private List<Point> _lstPointOnTable;
        private void CallstPointOnTable()
        {
            int i;
            int j;
            List<Point> lst = new List<Point>();
            for (i = 0; i < this.Size; i++)
            {
                for (j = 0; j < this.Size; j++)
                {
                    if (this.Tableint.Rows[i].Cols[j] == 1)
                    {
                        Point P = new Point(this.Position.X + j, this.Position.Y + i);
                        lst.Add(P);
                    }
                }
            }
            _lstPointOnTable = lst;
        }
        public List<Point> lstPointOnTable
        {
            get
            {
                
                return _lstPointOnTable;
            }
        }
        public GenericTable<int> GetCloneTable()
        {
            GenericTable<int> NewTable = new GenericTable<int>();
            int i;
            int j;
            for (i = 0; i < this.Size; i++)
            {
                Row<int> NewRow = new Row<int>();
                for (j = 0; j < this.Size; j++)
                {
                    int iNewValue = this.Tableint.Rows[i].Cols[j];
                    NewRow.Cols.Add(iNewValue);
                }
                NewTable.Rows.Add(NewRow);
            }
            return NewTable;
        }
        public Point _Position;
        public Point Position
        {
            get { return _Position; }
        }
        private Point _LowestPosition;
        public Point LowestPosition
        {
            get
            {
                return _LowestPosition;
            }
        }
        private void CalculateLowestPosition()
        {
            int i;
            int j;
            for (i = Size - 1; i >= 0; i--)
            {
                for (j = 0; j < Size; j++)
                {
                    if (Tableint.Rows[i].Cols[j] == 1)
                    {
                        _LowestPosition = new Point(j, i);
                        return;
                    }
                }
            }
        }
        public void Move(Game.enDirection dir)
        {
            switch (dir)
            {
                case Game.enDirection.Down:
                    _Position = new Point(_Position.X, _Position.Y + 1);
                    break;
                case Game.enDirection.Left :
                    _Position = new Point(_Position.X -1, _Position.Y);
                    break;
                case Game.enDirection.Right :
                    _Position = new Point(_Position.X + 1, _Position.Y);
                    break;

            }
            CallstPointOnTable();
        }
        private int GetCurrentRolateIndex()
        {
            if (lstRolation.Count == 1)
            {
                return 0;
            }
            if (_CurrentIndexRolation == lstRolation.Count - 1)
            {
                return 0;
            }
            else
            {
                return _CurrentIndexRolation + 1;
            }
        }
        
        public void Rolate()
        {
            if (_CurrentIndexRolation == lstRolation.Count - 1)
            {
                _CurrentIndexRolation = 0;
            }
            else
            {
                _CurrentIndexRolation++;
            }
            CalculateLowestPosition();
            CallstPointOnTable();
            //Tableint = lstRolation[_CurrentIndexRolation];
        }

        private int _CurrentIndexRolation = 0;

        public Block(Game.enBlockType pBlockType, Color pBColor, int pSize, List<GenericTable<int>> plstRolation)
        {
            _BlockType = pBlockType;
            _BColor = pBColor;
            _Size = pSize;
            
            _Position = new Point(0, 0);
            _lstRolation = plstRolation;
           // Tableint = _lstRolation[0];
            CalculateLowestPosition();
            CallstPointOnTable();
        }
    }
}
