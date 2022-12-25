using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Linq;


namespace KTetris
{
    public class Game
    {
        public event EventHandler ScoreChange;
        public event EventHandler GameFinished;
        private int _Row = 0;
        private int _Col = 0;
        public enum enGameStatus
        {
            Start = 0,
            Finish = 1,
            Edit = 2
        }
        private enGameStatus _GameMode = enGameStatus.Start ;
        public enGameStatus GameStaus
        {
            get { return _GameMode; }
        }

        public  enum enDirection
        {
            Up,
            Down,
            Left,
            Right
        }
        public  enum enBlockType
        {
            I,
            J,
            L,
            O,
            S,
            T,
            Z
        }
        private Dictionary<enBlockType, System.Drawing.Color> _DicColor;
        private Dictionary<enBlockType,System.Drawing.Color> DicColor
        {
            get
            {
                if (_DicColor == null)
                {
                    _DicColor = new Dictionary<enBlockType, System.Drawing.Color>();
                    _DicColor.Add(enBlockType.I, Color.LightBlue);
                    _DicColor.Add(enBlockType.J, Color.Blue);
                    _DicColor.Add(enBlockType.L, Color.Orange);
                    _DicColor.Add(enBlockType.O, Color.Yellow);
                    _DicColor.Add(enBlockType.S, Color.LightGreen);
                    _DicColor.Add(enBlockType.T, Color.Purple);
                    _DicColor.Add(enBlockType.Z, Color.Red);
                }
                return _DicColor;
            }
        }
        private System.Drawing.Color GetColorByBlockType(enBlockType e)
        {
            return DicColor[e];
        }
        private System.Drawing.Color GetColorByBlockType(string str)
        {
            enBlockType e = enBlockType.I;
            switch (str)
            {
                case "J":
                    e = enBlockType.J;
                    break;
                case "L":
                    e = enBlockType.L;
                    break;
                case "O":
                    e = enBlockType.O;
                    break;
                case "S":
                    e = enBlockType.S;
                    break ;
                case "T":
                    e = enBlockType.T;
                    break ;
                case "Z":
                    e = enBlockType.Z;
                    break;
            }
            return GetColorByBlockType(e);
        }
        public int Row
        {
            get { return _Row; }
        }

        public int Col
        {
            get { return _Col; }
        }
        private DoubleBufferedPanel _PnlDisplayNextBlock;
        private DoubleBufferedPanel PanDisplayNextBlock
        {
            get { return _PnlDisplayNextBlock; }
        }
        private DoubleBufferedPanel _Pnl;
        private DoubleBufferedPanel Pnl
        {
            get { return _Pnl; }
        }
        private PanelGrid _PnlGridDisplayNextBlock;
        private PanelGrid PnlGridDisplayNextBlock
        {
            get { return _PnlGridDisplayNextBlock; }
        }
        private PanelGrid _PnlGrid;
        private PanelGrid PnlGrid
        {
            get { return _PnlGrid; }
        }
        private void LogNewblock()
        {
            List<Block> lst = queNextBlock.ToList();
            int i;
            Util.WriteLog("LogNewBlock");
            for (i = 0; i < lst.Count; i++)
            {
                Util.WriteLog(lst[i].BlockType.ToString());
            }
            Util.WriteLog("EndLogNewBlock");
        }
        private void LoadNewBlockIntolstNextBlock()
        {
            while (queNextBlock.Count <= this.MaxNextBlock)
            {
                LoadNewBlock();
            }
            this.RenderNextBlock();
        }
        private void InitialState()
        {
            this.LoadNewBlockIntolstNextBlock();
            this.GetCurrentBlockFromQuery();
            
        }
        private int MaxNextBlock = 4;
        private Queue<Block> _queNextBlock = new Queue<Block>();
        private Queue<Block> queNextBlock
        {
            get { return _queNextBlock; }
        }
        private Block  _CurrentBlock;
        private Block  CurrentBlock
        {
            get { 
                return _CurrentBlock; 
       //         return queNextBlock.ToList()[0];
            }
        }
        private void InitialTableT(GenericTable<int> table, int size)
        {
            int i;
            int j;
            //table = new cTableT<int>();
            for (i = 0; i < size; i++)
            {
                Row<int> NewRow = new Row<int>();
                table.Rows.Add(NewRow);
                for (j = 0; j < size; j++)
                {
                    NewRow.Cols.Add(0);
                }
            }
        }
        private void ClearTable(GenericTable<int> pTable)
        {
            int i;
            int j;
            for (i = 0; i < pTable.Rows.Count; i++)
            {
                for (j = 0; j < pTable.Rows[0].Cols.Count; j++)
                {
                    pTable.Rows[i].Cols[j] = 0;
                }
            }
        }
      
        private void LoadNewBlock()
        {
            
            int iRandom= Util.GetRandom(0, 6);
            enBlockType eBlockType = (enBlockType)iRandom;
       
            Point FirstPoint=new Point(0,0);
          
           
            GenericTable <int> intTable=new GenericTable<int> ();
            string strTemp = "";
            int Size=0;
            List<GenericTable<int>> lstRolation = new List<GenericTable<int>>();
            switch (eBlockType)
            {
                case enBlockType.I:
                    Size=4;
                InitialTableT(intTable, Size );
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         intTable.Rows[1].Cols[3] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[2] = 1;
         intTable.Rows[1].Cols[2] = 1;
         intTable.Rows[2].Cols[2] = 1;
         intTable.Rows[3].Cols[2] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[2].Cols[0] = 1;
         intTable.Rows[2].Cols[1] = 1;
         intTable.Rows[2].Cols[2] = 1;
         intTable.Rows[2].Cols[3] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[2].Cols[1] = 1;
         intTable.Rows[3].Cols[1] = 1;
         lstRolation.Add(intTable);
                    
                    break;
                case enBlockType.J:
                    Size=3;
                    InitialTableT(intTable, Size );
                    intTable.Rows[0].Cols[1] = 1;
                    intTable.Rows[1].Cols[1] = 1;
                    intTable.Rows[2].Cols[0] = 1;
                    intTable.Rows[2].Cols[1] = 1;
                    lstRolation.Add(intTable);
                    intTable = new GenericTable<int>();
                    InitialTableT(intTable, Size );
                    intTable.Rows[0].Cols[0] = 1;
                    intTable.Rows[1].Cols[0] = 1;
                    intTable.Rows[1].Cols[1] = 1;
                    intTable.Rows[1].Cols[2] = 1;
                    lstRolation.Add(intTable);
                    intTable = new GenericTable<int>();
                    InitialTableT(intTable, Size );
                    intTable.Rows[0].Cols[1] = 1;
                    intTable.Rows[0].Cols[2] = 1;
                    intTable.Rows[1].Cols[1] = 1;
                    intTable.Rows[2].Cols[1] = 1;
                    lstRolation.Add(intTable);
                    intTable = new GenericTable<int>();
                    InitialTableT(intTable, Size );
                    intTable.Rows[1].Cols[0] = 1;
                    intTable.Rows[1].Cols[1] = 1;
                    intTable.Rows[1].Cols[2] = 1;
                    intTable.Rows[2].Cols[2] = 1;
                    lstRolation.Add(intTable);

                    break;
                case enBlockType.L :
                              Size=3;
                           InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[2] = 1;
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[2].Cols[1] = 1;
         intTable.Rows[2].Cols[2] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         intTable.Rows[2].Cols[0] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[0] = 1;
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[2].Cols[1] = 1;
         lstRolation.Add(intTable);
                    break;
                case enBlockType.O:
                    Size=2;
                    InitialTableT(intTable, Size );
                    intTable.Rows[0].Cols[0] = 1;
                    intTable.Rows[0].Cols[1] = 1;
                    intTable.Rows[1].Cols[0] = 1;
                    intTable.Rows[1].Cols[1] = 1;
                    lstRolation.Add(intTable);
                    break;
                case enBlockType.S :
                    Size=3;
                     InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[0].Cols[2] = 1;
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         intTable.Rows[2].Cols[2] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         intTable.Rows[2].Cols[0] = 1;
         intTable.Rows[2].Cols[1] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[0] = 1;
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[2].Cols[1] = 1;
         lstRolation.Add(intTable);
                    break;
                case enBlockType.T :
                            Size=3;
                          InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         intTable.Rows[2].Cols[1] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         intTable.Rows[2].Cols[1] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[2].Cols[1] = 1;
         lstRolation.Add(intTable);
                  
                    break;
                case enBlockType.Z :
                              Size=3;
                     InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[0] = 1;
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[2] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[1].Cols[2] = 1;
         intTable.Rows[2].Cols[1] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[2].Cols[1] = 1;
         intTable.Rows[2].Cols[2] = 1;
         lstRolation.Add(intTable);
         intTable = new GenericTable<int>();
         InitialTableT(intTable, Size );
         intTable.Rows[0].Cols[1] = 1;
         intTable.Rows[1].Cols[0] = 1;
         intTable.Rows[1].Cols[1] = 1;
         intTable.Rows[2].Cols[0] = 1;
         lstRolation.Add(intTable);

                    break;

            }
            Block NewBlock = new Block(eBlockType, GetColorByBlockType(eBlockType), Size,lstRolation );
            //lstNextBlock.Insert(0, NewBlock);
            //queNextBlock.Add(NewBlock);
            queNextBlock.Enqueue(NewBlock);
            //_CurrentBlock = queNextBlock.Dequeue();

            //_CurrentBlock = NewBlock;
           //
        }
        private void GetCurrentBlockFromQuery()
        {
            _CurrentBlock = queNextBlock.Dequeue();

        }
        private bool IsValidMove(Point P)
        {
            if (P.X < 0 ||
                P.Y < 0 ||
                P.X >= this.Col ||
                P.Y >= this.Row)
            {
                return false;
            }
            return true;
        }
        public bool CanMove(Point NewPoint)
        {
            int i;
            for (i = 0; i < this.CurrentBlock.lstPointOnTable.Count; i++)
            {
                Point tempPoint = new Point(this.CurrentBlock.lstPointOnTable[i].X + NewPoint.X,
                                             this.CurrentBlock.lstPointOnTable[i].Y + NewPoint.Y);
                /*
                if (tempPoint.X < 0 ||
                    tempPoint.Y < 0 ||
                    tempPoint.X >= this.Col ||
                    tempPoint.Y >= this.Row)
                {
                    return false;
                }
                */
                if (tempPoint.X < 0 ||
                    tempPoint.X >= this.Col ||
                    tempPoint.Y < 0 ||
                    tempPoint.Y >= this.Row)
                {
                    return false;
                }

                if (this.Table.Cell(tempPoint).Value != "")
                {
                    return false;
                }
            }
            return true;
        }
        public bool CanRolateBlock()
        {
            int i;
            int j;

            for (i = 0; i < this.CurrentBlock.Size; i++)
            {
                for (j = 0; j < this.CurrentBlock.Size; j++)
                {
                    if (this.CurrentBlock.RolateTableint.Rows[i].Cols[j] == 1)
                    {
                        int iRowOnTable = this.CurrentBlock.Position.Y + i;
                        int iColOnTable = this.CurrentBlock.Position.X + j;
                        if (iRowOnTable < 0 ||
                            iRowOnTable >= this.Row ||
                            iColOnTable < 0 ||
                            iColOnTable >= this.Col)
                        {
                            return false;
                        }

                        if (this.Table.Rows[iRowOnTable].Cols[iColOnTable].Value != "")
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
            // this.CurrentBlock.RolateTableint 
        }

        public void RolateBloack()
        {
            if (!(CanRolateBlock()))
            {
                return;
            }
            this.CurrentBlock.Rolate();
            this.RenderCurrentBlock();
            this.CheckHasTouchedTheBottom();
        }
        public bool HasTouchedTheExistedBlock()
        {

            int i;
            int j;
            for (i = 0; i < CurrentBlock.Size; i++)
            {
                for (j = 0; j < CurrentBlock.Size; j++)
                {
                    if (CurrentBlock.Tableint.Rows[i].Cols[j] == 1)
                    {
                        int iRowOnTable = CurrentBlock.Position.Y + i;
                        int iColOnTable = CurrentBlock.Position.X + j;
                        try
                        {
                            if (this.Table.Rows[iRowOnTable + 1].Cols[iColOnTable].Value != "")
                            {
                                return true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Util.WriteErrorLog(ex);
                        }
                    }
                }
            }
            return false;
        }
        private bool _HasTouchedTheBottom;
        private bool HasTouchedTheBottom
        {
            get { return _HasTouchedTheBottom; }
        }

        public void  CheckHasTouchedTheBottom()
        {
            if (CurrentBlock.LowestPosition.Y + CurrentBlock.Position.Y >= this.Row - 1)
            {
                _HasTouchedTheBottom = true;
                //return true;
            }
            else
            {
                _HasTouchedTheBottom = false;
            }
            //return false;
        }
        private void KeepCurentBlockValueToTable()
        {
            int i;
            int j;
            for (i = 0; i < CurrentBlock.Size; i++)
            {
                for (j = 0; j < CurrentBlock.Size; j++)
                {
                    if (CurrentBlock.Tableint.Rows[i].Cols[j] == 1)
                    {
                        Point CurrentPosition = CurrentBlock.Position;
                        Cell c = Table.Rows[CurrentPosition.Y + i].Cols[CurrentPosition.X + j];
                        c.BackColor = CurrentBlock.BColor;
                        c.HasBorder = true;
                        c.Value = CurrentBlock.BlockType.ToString ();
                    }
                }
            }
        }
        public void FallCell()
        {
            while (!MoveCell(enDirection.Down))
            {

            }
        }
        public bool MoveCell(enDirection Direction)
        {
            if (CurrentBlock == null)
            {
                return false ;
            }
            Point NewPoint=new Point(0,0);
            switch (Direction)
            {
                case enDirection.Down :
                    NewPoint = new Point(0, 1);
                    break;
                case enDirection.Left :
                    NewPoint = new Point(-1, 0);
                    break;
                case enDirection.Right :
                    NewPoint = new Point(1, 0);
                    break;
            }
            int i;
            bool IsFinishedForThisBlock = false;
            if (!(CanMove(NewPoint)))
            {
                if (Direction == enDirection.Down)
                {
                    IsFinishedForThisBlock = true;
                }
           
            }
            else
            {
                CurrentBlock.Move(Direction);
            }
          

            
            if (HasTouchedTheExistedBlock())
            {
                IsFinishedForThisBlock = true;
            }
            /*
            if (HasTouchedTheBottom)
            {
                if (Direction == enDirection.Down)
                {
                    IsFinishedForThisBlock = true;
                }                            
            }
            else
            {
                CheckHasTouchedTheBottom();
            }
            */

            if (IsFinishedForThisBlock)
            {
                KeepCurentBlockValueToTable();
                RemoveCompletedRow();
              
                //_CurrentBlock = null;
                LoadNewBlock();
                GetCurrentBlockFromQuery();
                this.RenderNextBlock();
                return true;
            }
            return false;

            /*
            List<Point> lTempPoint = new List<Point>();
            for (i = 0; i < CurrentBlock.Count; i++)
            {
                Point lPoint;
                lPoint  = new Point(CurrentBlock[i].X + NewPoint.X, CurrentBlock[i].Y + NewPoint.Y);
                if (!IsValidMove(lPoint ))
                {
                    return;
                }
                lTempPoint.Add(lPoint);
            }

            for (i = 0; i < CurrentBlock.Count; i++)
            {
                CurrentBlock[i] = new Point(lTempPoint [i].X ,lTempPoint [i].Y);
            }
            */

        }
        private bool _IsGameSleeping = false;
        public bool IsGameSleeping
        {
            get { return _IsGameSleeping; }
        }
        private void Sleep(int millisecond)
        {
            _IsGameSleeping = true;
            System.Threading.Thread.Sleep(millisecond);
            _IsGameSleeping = false;
        }
        private int _LatestRemovedRows;
        private int LatestRemovedRows
        {
            get
            {
                return _LatestRemovedRows;
            }
        }
        private void RemoveCompletedRow()
        {
            int i;
            int j;
            List<int> lstRowIndexNeedToBeRemoved = new List<int>();
            for (i = 0; i < CurrentBlock.lstPointOnTable.Count; i++)
            {
                Point P = CurrentBlock.lstPointOnTable[i];
                bool IsThisLineCompledted = true;
                if (lstRowIndexNeedToBeRemoved.Contains(P.Y))
                {
                    IsThisLineCompledted = false;
                }
                for (j = 0; j < this.Col; j++)
                {
                    if (this.Table.Rows[P.Y].Cols[j].Value == "")
                    {
                        IsThisLineCompledted = false;
                        break;
                    }
                    
                }
                if (IsThisLineCompledted)
                {
                    lstRowIndexNeedToBeRemoved.Add(P.Y);
                }
            }
            lstRowIndexNeedToBeRemoved.Sort();
            for (i = 0; i < lstRowIndexNeedToBeRemoved.Count; i++)
            {
                int iIndexRowRemove = lstRowIndexNeedToBeRemoved[i];
                for (j = 0; j < this.Col; j++)
                {
                    Cell c = this.Table.Rows[iIndexRowRemove].Cols[j];
                    c.BackColor = Color.White;
                    c.Value = "";
                  //  Sleep(300);
                }

              
            }
            for (i = 0; i < lstRowIndexNeedToBeRemoved.Count; i++)
            {
                int k = 0;
                int iIndexRowRemove = lstRowIndexNeedToBeRemoved[i];
                for (k = iIndexRowRemove; k > 0; k--)
                {
                    for (j = 0; j < this.Col; j++)
                    {
                        Cell cNewCell = this.Table.Rows[k].Cols[j];
                        Cell cUpperCell = this.Table.Rows[k - 1].Cols[j];
                        cNewCell.Value = cUpperCell.Value;
                        cNewCell.BackColor = GetColorByBlockType(cNewCell.Value);
                      //  Sleep(20);
                    }

                }
                for (j = 0; j < this.Col; j++)
                {
                    Table.Rows[0].Cols[j].Value = "";
                    Table.Rows[0].Cols[j].BackColor = Color.White;
                   // Sleep(20);
                }
            }
            _LatestRemovedRows = lstRowIndexNeedToBeRemoved.Count;
            _Score += CalScore(lstRowIndexNeedToBeRemoved.Count);
            _Lines += lstRowIndexNeedToBeRemoved.Count;
            ScoreChange(this, null);
        }

        private TableCell Table
        {
            get
            {
                return this.PnlGrid.Table;
            }
        }
        private void RenderNextBlock()
        {
            int i;
            int j;
            int k;
            int iRowPosition = 1;
            List<Block> lstBlock = this.queNextBlock.ToList();
            foreach(Cell cT in this.PnlGridDisplayNextBlock.Table.Cells )
            {
                cT.Value = "";
                cT.BackColor = Color.Black ;
                cT.HasBorder = false;
            }
            for (i = 0; i < lstBlock.Count; i++)
            {
                Block c = lstBlock[i];
                for (j = 0; j < c.Size; j++)
                {
                    for (k = 0; k < c.Size; k++)
                    {
                        if (c.Tableint.Rows[j].Cols[k] > 0)
                        {
                            this.PnlGridDisplayNextBlock.Table.Rows[j + iRowPosition].Cols[k + 1].BackColor = c.BColor;
                            this.PnlGridDisplayNextBlock.Table.Rows[j + iRowPosition].Cols[k + 1].HasBorder = true;
                        }
                        else
                        {
                          //  this.PnlGridDisplayNextBlock.Table.Rows[j + iRowPosition].Cols[k].BackColor = Color.LightBlue;
                        }
                    }
                }
                iRowPosition += c.Size + 1;
            }
        }
        
        private void RenderWholeTable()
        {
            int i;
            int j;
            for (i = 0; i < Table.Rows.Count; i++)
            {
                for (j = 0; j < Table.Rows[0].Cols.Count; j++)
                {
                    Cell c = Table.Rows[i].Cols[j];
                    if (c.Value == "")
                    {
                        c.BackColor = Color.White;
                        c.HasBorder = false;
                    }
                    else
                    {
                        c.HasBorder = true;
                        c.BackColor = GetColorByBlockType(c.Value);
                    }
                }
            }

        }
        private void RenderCurrentBlock()
        {
            int i;
            int j;

          

            
            if (CurrentBlock == null)
            {
                return;
            }


            for (i = 0; i < CurrentBlock.Size; i++)
            {
                for (j = 0; j < CurrentBlock.Size; j++)
                {
                    if (CurrentBlock.Tableint.Rows[i].Cols[j] == 1)
                    {
                        if (this.Table.Rows[i].Cols[j].Value != "")
                        {

                            
                            if (GameFinished != null)
                            {
                                this._GameMode  = enGameStatus.Finish;
                                GameFinished(this, null);
                            }
                            return;
                        }
                        try
                        {
                            Table.Rows[CurrentBlock.Position.Y + i].Cols[CurrentBlock.Position.X + j].BackColor = CurrentBlock.BColor;
                            Table.Rows[CurrentBlock.Position.Y + i].Cols[CurrentBlock.Position.X + j].HasBorder = true;
                        }
                        catch (Exception ex)
                        {
                            Util.WriteErrorLog(ex);
                        }
                    }
                    else
                    {
                        /*
                        Table.Rows[CurrentBlock.Position.Y + i].Cols[CurrentBlock.Position.X + j].BackColor = Color.White;
                        Table.Rows[CurrentBlock.Position.Y + i].Cols[CurrentBlock.Position.X + j].HasBorder = true;
                         */
                    }
                }
            }
            

            

        }
        public void Loop()
        {
            this.RenderWholeTable();
            this.RenderCurrentBlock();
            
        }
        private int _Lines;
        public int Lines
        {
            get { return _Lines; }
        }
        private int _Score;
        public int Score
        {
            get { return _Score; }
        }
        private int _Level = 1;
        private int CalScore(int NumberofLine)
        {
            int tempAddScore = 0;
            tempAddScore = NumberofLine * NumberofLine * _Level;
            return tempAddScore;

        }
        public int Level
        {
            get { return _Level; }
        }


        public Game(DoubleBufferedPanel  PPnl,DoubleBufferedPanel PPnlDisplayNextBlock, int pRow, int pCol)
        {
            _Pnl = PPnl;
            _PnlDisplayNextBlock = PPnlDisplayNextBlock;
            _Row = pRow;
            _Col = pCol;
            _PnlGrid = new PanelGrid(_Pnl, pRow, pCol, 32);
            _PnlGridDisplayNextBlock = new PanelGrid(_PnlDisplayNextBlock, 45, 6, 32);


            //_PnlGrid.CellClickEvent += new cPanelGrid.CellClickEventHandler(PnlGrid_CellClickEvent);
            _Score = 0;
            this.InitialState();
        }
    }
}
