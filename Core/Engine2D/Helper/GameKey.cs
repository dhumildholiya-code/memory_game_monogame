using Microsoft.Xna.Framework.Input;

namespace Core.Engine2D.Helper
{
    public struct GameKey
    {
        public Keys Key;
        public bool down;
        public bool pressed;
        public bool released;

        public GameKey(Keys key)
        {
            this.Key = key;
            down = false;
            pressed = false;
            released = false;
        }
    }
}
