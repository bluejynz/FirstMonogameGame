using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

class ThreeColorGameObject
{
    //variables
    protected Vector2 position, origin, velocity;
    protected Texture2D colorRed, colorGreen, colorBlue;
    Color color;
    protected float rotation;

    //constructors
    protected ThreeColorGameObject(ContentManager Content, string redSpriteName, string greenSpriteName, string blueSpriteName)
    {
        colorRed = Content.Load<Texture2D>(redSpriteName);
        colorGreen = Content.Load<Texture2D>(greenSpriteName);
        colorBlue = Content.Load<Texture2D>(blueSpriteName);

        origin = new Vector2(colorRed.Width, colorRed.Height) / 2;

        position = Vector2.Zero;
        velocity = Vector2.Zero;
        rotation = 0;

        Reset();
    }

    //props
    public Rectangle BoundingBox
    {
        get
        {
            Rectangle spriteBounds = colorRed.Bounds;
            spriteBounds.Offset(position - origin);
            return spriteBounds;
        }
    }

    public Color Color
    {
        get { return color; }
        protected set
        {
            if (value != Color.Red && value != Color.Green && value != Color.Blue)
            {
                return;
            }
            color = value;
        }
    }

    public Vector2 Position
    {
        get { return position; }
    }

    //methods

    public virtual void HandleInput(InputHelper inputHelper) {}

    public virtual void Update(GameTime gameTime)
    {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Texture2D currentSprite;
        if (Color == Color.Red)
            currentSprite = colorRed;
        else if (Color == Color.Green)
            currentSprite = colorGreen;
        else
            currentSprite = colorBlue;

        spriteBatch.Draw(currentSprite, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
    }

    public virtual void Reset()
    {
        Color = Color.Blue;
    }
}
