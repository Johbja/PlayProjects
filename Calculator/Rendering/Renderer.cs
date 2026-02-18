using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Rendering;
internal class Renderer
{
    public static readonly Size CanvasSize = new Size(256*2, 256*2);
    public static readonly int FPS = 120;
    public static readonly int FrameTimeMs = 1000 / FPS;

    private List<RenderObject> renderObjects = new();

    public Renderer()
    {
        renderObjects.Add(RenderObject.CreateCube());
    }

    public void StartRendering()
    {
        OpenCanvasWindow();
    }

    private void RenderStep()
    {
        foreach(var renderObject in renderObjects)
        {
            renderObject.Update();
        }
    }

    private void OpenCanvasWindow()
    {
        var thread = new Thread(() =>
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new CanvasForm();
            form.ClientSize = CanvasSize;
            form.Paint += FormPaint;
            
            foreach(var renderObject in renderObjects)
            {
                form.Paint += renderObject.PaintObject;
            }

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = FrameTimeMs;
            timer.Tick += (s, e) =>
            {
                RenderStep();
                form.Invalidate();
            };
            timer.Start();

            Application.Run(form);
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    private void FormPaint(object? sender, PaintEventArgs e)
    {
        //var random = new Random();
        //var color = System.Drawing.Color.FromArgb(
        //    255, // fully opaque
        //    random.Next(256),
        //    random.Next(256),
        //    random.Next(256)
        //);

        e.Graphics.FillRectangle(Brushes.Black, 0, 0, CanvasSize.Width, CanvasSize.Height);
        //e.Graphics.FillEllipse(Brushes.AliceBlue, 50, 50, 100, 100);
        //e.Graphics.DrawLine(Pens.Red, 0, 0, 256 * 2, 256 * 2);
    }
}
