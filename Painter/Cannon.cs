using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

class Cannon : ThreeColorGameObject
{
    //variables
    Texture2D cannonBarrel;
    Vector2 barrelOrigin;
    float barrelRotation;

    //constructors
    public Cannon(ContentManager Content) : base(Content, "spr_cannon_red", "spr_cannon_green", "spr_cannon_blue")
    {
        cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");

        barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2;
        position = new Vector2(72, 405);
    }

    //props
    public Vector2 BallPosition
    {
        get
        {
            float opposite = (float)Math.Sin(barrelRotation) * cannonBarrel.Width * 0.75f;
            float adjacent = (float)Math.Cos(barrelRotation) * cannonBarrel.Width * 0.75f;
            return Position + new Vector2(adjacent, opposite);
        }
    }

    //methods
    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.R))
        {
            Color = Color.Red;
        }
        else if (inputHelper.KeyPressed(Keys.G))
        {
            Color = Color.Green;
        }
        else if (inputHelper.KeyPressed(Keys.B))
        {
            Color = Color.Blue;
        }

        double opposite = inputHelper.MousePosition.Y - Position.Y;
        double adjacent = inputHelper.MousePosition.X - Position.X;
        barrelRotation = (float)Math.Atan2(opposite, adjacent);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(cannonBarrel, Position, null, Color.White, barrelRotation, barrelOrigin, 1f, SpriteEffects.None, 0);
        base.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        base.Reset();
        barrelRotation = 0.0f;
    }
}
