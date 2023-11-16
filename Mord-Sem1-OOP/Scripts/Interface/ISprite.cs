using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MordSem1OOP.Scripts.Interface
{
    public interface ISprite
    {
        int Width { get; }
        int Height { get; }
        float Rotation { get; set; }
        float DepthLayer { get; set; }
        Vector2 Origin { get; }
        Color Color { get; set; }

        Rectangle Rectangle { get; }

        void Draw(Vector2 position, float rotation, float scale);
        //void IndepententDraw(Vector2 position, float rotation, float scale); 
        void SetOrigin(Vector2 origin);
        void SetOriginCenter();
    }
}
