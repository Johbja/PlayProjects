using System.Numerics;

namespace Calculator.Rendering;

internal class RenderObject
{
    private bool renderVerts;
    private int vertSize;
    private float timeElapsed;

    private Vector3[] verts;
    private int[][] faces;


    public RenderObject(Vector3[] verts, int[][] faces, bool renderVerts = false, int vertSize = 6)
    {
        this.verts = verts;
        this.faces = faces;
        this.renderVerts = renderVerts;
        this.vertSize = vertSize;
    }

    private (Vector2 pos, int size) ScreenPoint(Vector2 pos, int size)
    {
        var offset = size / 2;
        return ((pos - Vector2.One * offset, size));
    }

    private Vector2 TranslateToScreenCoordinates(Vector2 worldPos)
    {
        return new Vector2(
            ((worldPos.X + 1) / 2) * Renderer.CanvasSize.Width,
            (1 - (worldPos.Y + 1) / 2) * Renderer.CanvasSize.Height);
    }

    private Vector2 ProjectToScreen(Vector3 position)
    {
        return new Vector2(
            (position.X / position.Z),
            (position.Y / position.Z));
    }

    private Vector3 TranslateZ(Vector3 pos, float amount)
    {
        return pos + Vector3.UnitZ * amount;
    }

    private Vector3 RotateXZ(Vector3 pos, float angle, Vector3 center)
    {
        var centeredPos = pos - center;

        float cos = MathF.Cos(angle);
        float sin = MathF.Sin(angle);
        var rotatedPos = new Vector3(
            centeredPos.X * cos - centeredPos.Z * sin,
            centeredPos.Y,
            centeredPos.X * sin + centeredPos.Z * cos
        );

        return rotatedPos + center;
    }

    private void Line(PaintEventArgs e, Vector2 start, Vector2 end)
    {
        e.Graphics.DrawLine(Pens.Green, start.X, start.Y, end.X, end.Y);
    }

    public void PaintObject(object? sender, PaintEventArgs e)
    {
        if (renderVerts)
        {
            foreach (var vert in verts)
            {
                var point = ScreenPoint(TranslateToScreenCoordinates(ProjectToScreen(vert)), vertSize);

                e.Graphics.FillRectangle(Brushes.Green, point.pos.X, point.pos.Y, point.size, point.size);
            }
        }


        foreach (var face in faces)
        {
            for (int i = 0; i < face.Length; i++)
            {
                var vertConnectionA = verts[face[i]];
                var vertConnectionB = verts[face[(i + 1) % face.Length]];

                var pointA = TranslateToScreenCoordinates(ProjectToScreen(vertConnectionA));
                var pointB = TranslateToScreenCoordinates(ProjectToScreen(vertConnectionB));

                Line(e, pointA, pointB);
            }
        }
    }

    public void Update()
    {
        float dt = 1f / Renderer.FPS;
        timeElapsed += dt;
        var objectCenter = verts.Aggregate(Vector3.Zero, (acc, vert) => acc + vert) / verts.Length;

        var zOffsetValue = MathF.Cos(timeElapsed) * dt;

        for (var i = 0; i < verts.Length; i++)
        {
            verts[i] = TranslateZ(RotateXZ(verts[i], MathF.PI * dt, objectCenter), zOffsetValue);
        }
    }
    public static RenderObject CreateCube()
    {
        Vector3[] verts =
        [
            new Vector3(-0.5f, -0.5f, 3f),
            new Vector3(0.5f, -0.5f, 3f),
            new Vector3(0.5f, 0.5f, 3f),
            new Vector3(-0.5f, 0.5f, 3f),

            new Vector3(-0.5f, -0.5f, 2f),
            new Vector3(0.5f, -0.5f, 2f),
            new Vector3(0.5f, 0.5f, 2f),
            new Vector3(-0.5f, 0.5f, 2f)
        ];

        int[][] faces =
        [
            [0, 1, 2, 3],
            [4, 5, 6, 7],
            [0, 4],
            [1, 5],
            [2, 6],
            [3, 7]
        ];

        return new RenderObject(verts, faces);
    }

}
