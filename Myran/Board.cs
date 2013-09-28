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
        private bool sizeChanged = true;
        private Thread thread;
        private bool enabled = true;
        private Random random = new Random();
        private Queue<KeyValuePair<int, int>> changedSquares = new Queue<KeyValuePair<int, int>>();
        private Queue<KeyValuePair<int, int>> oldChangedSquares;

        public Board()
            : base()
        {
            this.columns = this.Width >> 4;
            this.rows = this.Height >> 4;
            this.Image = new Bitmap(this.Width, this.Height);
            //this.nodes = new Node[this.columns, this.rows];
            //this.nodes[1, 1] = new Ant(this, random);

            InitializeComponent();
        }

        public Board(IContainer container)
            : base()
        {
            this.columns = this.Width >> 4;
            this.rows = this.Height >> 4;
            this.Image = new Bitmap(this.Width, this.Height);
            //this.nodes = new Node[this.columns, this.rows];
            //this.nodes[1, 1] = new Ant(this, random);

            container.Add(this);

            InitializeComponent();
        }

        public Board(int columns, int rows)
        {

            //this.Image = new Bitmap(34, 34);
            //this.Columns = columns;
            //this.Rows = rows;
            this.nodes[0, 0] = new Ant(this, random);
        }

        public bool SetSquare(int x, int y, Node square)
        {
            if (x >= 0 && y >= 0 && x < columns && y < rows)
            {
                nodes[x, y] = square;
                changedSquares.Enqueue(new KeyValuePair<int, int>(x, y));
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

        public void Update()
        {
            if (this.enabled)
            {

                if (this.sizeChanged || true)
                {
                    if (nodes == null)
                    {
                        this.columns = this.Width >> 0;
                        this.rows = this.Height >> 0;
                        this.Image = new Bitmap(this.Width, this.Height);
                        this.nodes = new Node[this.columns, this.rows];
                        for (int i = 0; i < 200; i++)
                        {
                            int x = random.Next(columns);
                            int y = random.Next(rows);
                            this.nodes[x, y] = new Ant(this, random);
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
                    oldChangedSquares = changedSquares;
                    changedSquares = new Queue<KeyValuePair<int, int>>();

                    while (oldChangedSquares.Count > 0)
                    {
                        var pos = oldChangedSquares.Dequeue();
                        if (nodes[pos.Key, pos.Value] != null)
                            nodes[pos.Key, pos.Value].OnChange(this, random, pos.Key, pos.Value);
                    }
                }

                Bitmap bitmap = (Bitmap)this.Image;

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        if (nodes[x, y] != null)
                        {

                            nodes[x, y].Draw(x, y, ref bitmap);

                        }
                    }
                }
                this.Image = bitmap;
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

    }
}
