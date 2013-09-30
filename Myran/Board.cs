using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Myran
{
    public partial class Board : PictureBox
    {
        private Node[,] nodes;

        private int columns = 0;
        private int rows = 0;
        private int squareSize = 2;
        private bool sizeChanged = true;
        private Thread thread;
        private bool enabled = true;
        private Random random = new Random();
        private Stack<Position> changedSquares = new Stack<Position>();
        private Stack<Position> oldChangedSquares;
        private Stack<Position> squaresToDraw = new Stack<Position>();
        private Bitmap bitmap;

        public Board()
            : base()
        {
            //this.Clear();
            //this.columns = this.Width/squareSize;
            //this.rows = this.Height / squareSize;
            //this.bitmap = new Bitmap(this.Width, this.Height);
            //this.nodes = new Node[this.columns, this.rows];
            //this.nodes[1, 1] = new Ant(this, random);

            InitializeComponent();
        }

        public Board(IContainer container)
            : base()
        {
            //this.Clear();
            //this.columns = this.Width/squareSize;
            //this.rows = this.Height/squareSize;
            //this.bitmap = new Bitmap(this.Width, this.Height);
            //this.nodes = new Node[this.columns, this.rows];
            //this.nodes[1, 1] = new Ant(this, random);

            container.Add(this);

            InitializeComponent();
        }

        public Board(int columns, int rows)
        {
            //this.Clear();
            //this.Image = new Bitmap(34, 34);
            //this.Columns = columns;
            //this.Rows = rows;
            //this.nodes[0, 0] = new Ant(this, random);
        }

        public bool SetSquare(int x, int y, Node square)
        {
            if (x >= 0 && y >= 0 && x < columns && y < rows)
            {
                nodes[x, y] = square;
                changedSquares.Push(new Position(x, y));
                squaresToDraw.Push(new Position(x, y));
                return true;
            }
            return false;
        }

        public Node getSquare(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < columns && y < rows)
            {
                return nodes[x, y];
            }
            else
            {
                return null;
            }
        }

        public void SetPixel(int x, int y, Color color)
        {

        }

        public void Update()
        {
            if (this.enabled)
            {

                if (this.sizeChanged || true)
                {
                    if (nodes == null)
                    {
                        this.Reset(this.squareSize);
                    }
                    oldChangedSquares = changedSquares;
                    changedSquares = new Stack<Position>();

                    while (oldChangedSquares.Count > 0)
                    {
                        Position pos = oldChangedSquares.Pop();
                        if (nodes[pos.x, pos.y] != null)
                            nodes[pos.x, pos.y].OnChange(this, random, pos.x, pos.y);
                    }
                }

                /*for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        if (nodes[x, y] != null)
                        {

                            nodes[x, y].Draw(this, x, y, ref bitmap);

                        }
                    }
                }*/

                while (squaresToDraw.Count > 0)
                {
                    Position pos = squaresToDraw.Pop();
                    nodes[pos.x, pos.y].Draw(this, pos.x, pos.y, ref bitmap);
                }

                this.Image = bitmap;
            }
        }

        public void Reset(int squareSize)
        {
            this.squareSize = squareSize;
            this.columns = this.Width / squareSize;
            this.rows = this.Height / squareSize;
            this.bitmap = new Bitmap(this.Width, this.Height);
            this.Image = this.bitmap;
            this.nodes = new Node[this.columns, this.rows];

            changedSquares.Clear();
            oldChangedSquares = null;
            squaresToDraw.Clear();

            for (int i = 0; i > 16; i++)
            {
                int color = random.Next(6 * 256);

                int x = random.Next(columns);//columns / 16*7 + random.Next(columns/8);
                int y = random.Next(rows);//columns / 16*7 + random.Next(rows/8);

                for (int j = 0; j < random.Next(1,i*4+4); j++)
                {
                    this.nodes[(x+random.Next(32))%this.columns, (y+random.Next(32))%this.rows] = new Ant(this, random, color + random.Next(128));
                }
            }

            for (int j = 0; j < 16; j++)
            {
                int x = random.Next(columns);//columns / 16*7 + random.Next(columns/8);
                int y = random.Next(rows);//columns / 16*7 + random.Next(rows/8)
                this.nodes[x, y] = new Ant(this, random, random.Next(6 * 256));
            }

            for (int y = 0; y < this.rows; y++)
            {
                for (int x = 0; x < this.columns; x++)
                {
                    if (nodes[x, y] == null)
                    {
                        //nodes[x, y] = new Square(this, 0, random);
                    }
                    else
                    {
                        nodes[x, y].OnChange(this, random, x, y);
                    }
                }
            }
        }

        public int Columns
        {
            /*set
            {
                this.columns = value;
                sizeChanged = true;
                this.Width = columns;
                //this.Image = new Bitmap(this.columns, this.rows);
            }*/
            get
            {
                return this.columns;
            }
        }

        public int Rows
        {
            /*set
            {
                this.rows = value;
                sizeChanged = true;
                this.Height = rows;
                //this.Image = new Bitmap(this.Image, new Size(this.Width, this.Height));
            }*/
            get
            {
                return this.rows;
            }
        }

        public int SquareSize
        {
            get
            {
                return this.squareSize;
            }
        }

    }
}
