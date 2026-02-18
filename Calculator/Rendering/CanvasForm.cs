using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Rendering;
internal class CanvasForm : Form
{
    public CanvasForm()
    {
        Text = "Canvas Example";
        ClientSize = new Size(256*2, 256*2);
        DoubleBuffered = true;
    }
}
