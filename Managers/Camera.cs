using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GA_2d_shooter
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; set; }
        public float Zoom { get; set; }
        public float LerpFactor { get; set; }

        public Viewport viewport;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            Position = Vector2.Zero;
            Zoom = 0.9f;
            LerpFactor = 1f; //Controls smoothness
        }

        public void Follow(Player player)
        {
            if (player == null) return;

            Vector2 targetPosition = player.Position;
            Position = Vector2.Lerp(Position, targetPosition, LerpFactor);

            Transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }
    }
}
