/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Myran
{
    public class Boardd : PictureBox
    {
        private Node[,] nodes;

        private int columns = 0;
        private int rows = 0;
        private bool sizeChanged = true;
        private Bitmap bitmap;

        public Board()
            : base()
        {
        }

        public void Update()
        {
            if (sizeChanged)
            {
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        if (nodes[x, y] == null)
                        {
                            nodes[x, y] = new Square(this, 0);
                        }
                    }
                }
            }

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (nodes[x, y] == null)
                    {
                        nodes[x, y].Draw();
                    }
                }
            }
        }

        //ab dfefs
        public int Columns
        {
            //alol
            set { this.columns = value; sizeChanged = true; }
            //aldlo
            get { return this.columns; }
        }

        public int Rows
        {
            set { this.rows = value; sizeChanged = true; }
            get { return this.rows; }
        }
    }
}
*/