using System.Numerics;

namespace Calculator.Rendering;

internal class RenderObject(Vector3[] verts, int[][] faces, bool renderVerts = false, int vertSize = 6)
{
    public Vector3 Position { get; set; } = Vector3.UnitZ * 3;
    public Vector3 Rotation { get; set; } = Vector3.Zero;

    public event Action<RenderObject> OnUpdate;

    private float timeElapsed;

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


    private Vector3 ApplyTransform(Vector3 localVert)
    {
        var rotationMatrix = Matrix4x4.CreateRotationX(Rotation.X) 
                             * Matrix4x4.CreateRotationY(Rotation.Y) 
                             * Matrix4x4.CreateRotationZ(Rotation.Z);

        var vertRotated = Vector3.Transform(localVert, rotationMatrix);
        return vertRotated + Position;
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
                var appliedVert = ApplyTransform(vert);
                var point = ScreenPoint(TranslateToScreenCoordinates(ProjectToScreen(appliedVert)), vertSize);

                e.Graphics.FillRectangle(Brushes.Green, point.pos.X, point.pos.Y, point.size, point.size);
            }
        }


        foreach (var face in faces)
        {
            for (int i = 0; i < face.Length; i++)
            {
                var vertConnectionA = ApplyTransform(verts[face[i]]);
                var vertConnectionB = ApplyTransform(verts[face[(i + 1) % face.Length]]);

                var pointA = TranslateToScreenCoordinates(ProjectToScreen(vertConnectionA));
                var pointB = TranslateToScreenCoordinates(ProjectToScreen(vertConnectionB));

                Line(e, pointA, pointB);
            }
        }
    }

    public void Update()
    {
        OnUpdate?.Invoke(this);
    }

    public static RenderObject CreateCube()
    {
        Vector3[] verts =
        [
            new (-0.5f, -0.5f, 0.5f),
            new (0.5f, -0.5f, 0.5f),
            new (0.5f, 0.5f, 0.5f),
            new (-0.5f, 0.5f, 0.5f),

            new (-0.5f, -0.5f, -0.5f),
            new (0.5f, -0.5f, -0.5f),
            new (0.5f, 0.5f, -0.5f),
            new (-0.5f, 0.5f, -0.5f)
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

        var obj = new RenderObject(verts, faces);
        obj.OnUpdate += instance =>
        {
            float dt = 1f / Renderer.FPS;
            instance.timeElapsed += dt;

            var xOffset = MathF.Cos(instance.timeElapsed * 10) * dt * 0.5f;
            var yOffset = MathF.Sin(instance.timeElapsed * 10) * dt * 0.5f;
            //Position += new Vector3(xOffset, yOffset, 0);
            instance.Rotation += new Vector3(
                MathF.Cos(instance.timeElapsed * 10) * dt * 3f,
                MathF.Sin(instance.timeElapsed * 10) * dt * 3f, //MathF.PI * dt,
                dt
            );
        };

        return obj;
    }
}
