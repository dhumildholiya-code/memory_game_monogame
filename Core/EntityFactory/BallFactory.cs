using Core.Engine2D;
using Core.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.EntityFactory
{
    public static class BallFactory
    {
        public static Ball CreateBall(Texture2D texture, Color? color)
        {
            Transform transform = new Transform();
            Color spriteColor = color ?? Color.White;
            Sprite sprite = new Sprite(texture, spriteColor);
            return new Ball(transform, sprite);
        }
        public static Ball CreateBall(Texture2D texture, Vector2 position, Color? color)
        {
            Transform transform = new Transform(position);
            Color spriteColor = color ?? Color.White;
            Sprite sprite = new Sprite(texture, spriteColor);
            return new Ball(transform, sprite);
        }
    }
}
