using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms ;
using System.Drawing ;

    public class CellClickEventArgs:EventArgs 
    {
        private Cell _Cell;
        public CellClickEventArgs(Cell pCell) 
        {
            _Cell = pCell;
        }
        public Cell Cell {
            get { return _Cell; }
        } // readonly
    }
    public class PanelGrid
    {
        
        public   delegate void CellClickEventHandler(Cell  sender);
        public event CellClickEventHandler CellClickEvent;
        
        //public event EventHandler CellClickEvent;
        private int _NumberofRows = 0;
        private int _NumberofCols = 0;
        private int _CellBorderSize = 1;
        private int CellBorderSize
        {
            get { return _CellBorderSize; }
        }
        private void c_ColorChangeEvent(Cell sender)
        {
            if (!this.IsRenderingOntime)
            {
                return;
            }
        //this.RenderCell(P.CreateGraphics(), sender.Position.Y, sender.Position.X, sender.BackColor);
             this.RenderCell(P.CreateGraphics(), sender);
       // P.Update();
        //    P.Invalidate();
        }

        private void P_MouseDown(object sender, MouseEventArgs e)
        {
            int iCol = e.X / CellSize;
            int iRow = e.Y / CellSize;
            Cell c;
            try
            {
                 c = Table.Rows[iRow].Cols[iCol];
            }
            catch (Exception ex)
            {
                return;
            }
            CellClickEventHandler   MyHandle = CellClickEvent;

            //CellClickEventArgs MyArg = new CellClickEventArgs(c);
            if (MyHandle != null)
            {
                MyHandle(c);
            }

            /*
            if (Cell_Click != null)
            {
                Cell_Click(this.P, c);
            }
            */

            //MessageBox.Show(iRow.ToString() + "," + iCol.ToString());

        }
        private TableCell _Table = null;
        public TableCell Table
        {
            get {
                if (_Table == null)
                {
                    _Table = new TableCell();
                }
                return _Table; 
            }
        }
        private DoubleBufferedPanel _P;
        public DoubleBufferedPanel P
        {
            get{return _P ;}
        }
        private void InitalTable()
        {
            int i;
            int j;
            

            for (i = 0; i < _NumberofRows; i++)
            {
                RowCell RC = new RowCell();
                Table.Rows.Add(RC);
                for (j = 0; j < _NumberofCols; j++)
                {
                    Cell c = new Cell(new Point(j, i));
                    c.ColorChangeEvent+=new Cell.ColorChangeDelegate(c_ColorChangeEvent);
                    RC.Cols.Add(c);
                }
            }

        }
        private int CellSize = 0;
        public PanelGrid(DoubleBufferedPanel pP, int pRow, int pCol, int pCellSize)
        {
            /*
            this._P = pP;
            _NumberofCols = pCol;
            _NumberofRows = pRow;
            CellSize = pCellSize;
            this.InitalTable();
            this._P.Paint+=new PaintEventHandler(P_Paint);
            this._P.MouseDown+=new MouseEventHandler(P_MouseDown);
             */
            ExplicitConstructor(pP, pRow, pCol, pCellSize, 1);
        }
        private void ExplicitConstructor(DoubleBufferedPanel pP, int pRow, int pCol, int pCellSize, int pCellBorderSize)
        {  
            this._P = pP;
            _NumberofCols = pCol;
            _NumberofRows = pRow;
            _CellBorderSize = pCellBorderSize;
            CellSize = pCellSize;
            this.InitalTable();
            this._P.Paint += new PaintEventHandler(P_Paint);
            this._P.MouseDown += new MouseEventHandler(P_MouseDown);
        }
        public PanelGrid(DoubleBufferedPanel pP, int pRow, int pCol, int pCellSize,int pCellBorderSize)
        {
            ExplicitConstructor(pP, pRow, pCol, pCellSize, pCellBorderSize);
        }

        private void RenderCell(Graphics g, Cell Cell)
        {

            Rectangle RBackGroudForBorder = new Rectangle(Cell.Position.X  * this.CellSize,
                                      Cell.Position.Y  * this.CellSize,
                                      CellSize,
                                      CellSize);
            int localCellBorderSize = CellBorderSize;

            if (!Cell.HasBorder)
            {
                localCellBorderSize = 0;
            }
            Rectangle R = new Rectangle(RBackGroudForBorder.X + localCellBorderSize ,
                                      RBackGroudForBorder.Y + localCellBorderSize ,
                                      RBackGroudForBorder.Width - localCellBorderSize ,
                                      RBackGroudForBorder.Height - localCellBorderSize );

            if (Cell.HasBorder)
            {
                Brush BBorder = new SolidBrush(Color.Black);
                g.FillRectangle(BBorder, RBackGroudForBorder);
            }
            Brush Brush = new SolidBrush(Cell.BackColor );
            g.FillRectangle(Brush, R);



        }
        private void RenderCell(Graphics g,int Row,int Col, Color C)
        {
            
            Rectangle RBackGroudForBorder=new Rectangle (Col * this.CellSize ,
                                      Row * this.CellSize ,
                                      CellSize ,
                                      CellSize );
            Rectangle R = new Rectangle(RBackGroudForBorder.X + CellBorderSize ,
                                      RBackGroudForBorder.Y + CellBorderSize ,
                                      RBackGroudForBorder.Width - CellBorderSize ,
                                      RBackGroudForBorder.Height - CellBorderSize );

            Brush BBorder = new SolidBrush(Color.Black);
            g.FillRectangle(BBorder, RBackGroudForBorder);

            Brush Brush = new SolidBrush(C);
            g.FillRectangle(Brush, R);

            

        }
        private bool _IsRenderingOntime = true;

        public bool IsRenderingOntime
        {
            get { return _IsRenderingOntime; }
            set { 
                _IsRenderingOntime = value;
            if (_IsRenderingOntime)
            {
                //this.P.Invalidate();
                this.P.Update();
            }
            }
        }
        
        private void P_Paint(object sender, PaintEventArgs e)
        {
            if (!IsRenderingOntime)
            {
                return;
            }

            //Graphics g = this._P.CreateGraphics();
            Graphics g = e.Graphics;
            int i;
            int j;
                        

            for (i = 0; i < Table.Rows.Count ; i++)
            {
                for (j = 0; j < Table.Rows [0].Cols.Count ; j++)
                {
                    RenderCell(g, i,j, Table.Rows [i].Cols [j].BackColor );                  

                }
            }

        }

    }
    
    public class TableCell
    {
        public Cell Cell(Point P)
        {
            if (P.X < 0 ||
                P.Y < 0 ||
                P.X >= this.Rows[0].Cols.Count ||
                P.Y >= this.Rows.Count)
            {
                Util.WriteErrorLog (new Exception ("Point is invalid" + P.ToString ()));
                return null;
            }
            return this.Rows[P.Y].Cols[P.X];
        }
        public List<Cell> Cells
        {
            get
            {
                List<Cell> lstCell = new List<Cell>();
                int i;
                int j;
                for (i = 0; i < this.Rows.Count; i++)
                {
                    for (j = 0; j < this.Rows[0].Cols.Count; j++)
                    {
                        lstCell.Add(this.Rows[i].Cols[j]);
                    }
                }
                return lstCell;
            }

        }
        public List<RowCell> Rows = new List<RowCell>();
        public TableCell Clone()
        {
            int i;
            int j;
            TableCell NewTable = new TableCell();
            for (i = 0; i < this.Rows.Count; i++)
            {
                NewTable.Rows.Add (new RowCell ());
                for (j = 0; j < this.Rows[0].Cols.Count; j++)
                {
                    NewTable.Rows[i].Cols.Add(new Cell(new Point (0,0)));
                    NewTable.Rows[i].Cols[j] = this.Rows[i].Cols[j].Clone();
                }
            }
            return NewTable;
        }
    }
    public class RowCell
    {
        public List<Cell> Cols = new List<Cell>();


    }
    /*
    public class ColCell
    {
        public List<cCell> Item = new List<cCell>();
    }
     */

    public class Cell
    {
        public Cell Clone()
        {
            Cell NewCell = new Cell(this.Position);
            NewCell.BackColor = this.BackColor;
            NewCell.Value = this.Value;
            return NewCell;
        }
        private bool _HasUpdated = false;
        public bool HasUpdated
        {
            get { return _HasUpdated; }
            set { _HasUpdated = value; }
        }

        private  bool _HasBorder = true;
        public bool HasBorder
        {
            get { return _HasBorder; }
            set { _HasBorder = value;
            ColorChangeDelegate Eve = ColorChangeEvent;
            if (Eve != null)
            {
                Eve(this);
            }
            }
        }
        public delegate void ColorChangeDelegate(Cell sender) ;
        public event ColorChangeDelegate ColorChangeEvent;
        public Cell(Point pPosition)
        {
            this._Postion = pPosition;
        }

        private Point _Postion;
        public Point Position
        {
            get { return _Postion; }
        }

        private Color _BackColor;
        public Color BackColor
        {
            get { return _BackColor; }
            set { 
                _BackColor = value;
                ColorChangeDelegate  Eve = ColorChangeEvent;
                if (Eve != null)
                {
                    Eve(this);
                }
             }
        }
        private string _Value = "";
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

    }

