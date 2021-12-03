using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

class PaintCan : ThreeColorGameObject
{
    //variables
    Color targetColor;
    float minSpeed;
    Ball ball;
    SoundEffect scoreSound;

    //constructors
    public PaintCan(ContentManager Content, float positionOffset, Color target) : base(Content, "spr_can_red", "spr_can_green", "spr_can_blue")
    {
        scoreSound = Content.Load<SoundEffect>("snd_collect_points");
        position = new Vector2(positionOffset, -origin.Y);
        targetColor = target;
        ResetMinSpeed();
        Reset();
    }

    //methods
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        rotation = (float)Math.Sin(position.Y / 50.0) * 0.05f;

        if (ball == null)
            ball = Painter.GameWorld.Ball;
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        minSpeed += 0.01f * dt;
        if(velocity != Vector2.Zero)
        {
            position += velocity * dt;
            if (Painter.GameWorld.IsOutsideWorld(position - origin))
            {
                if (Color != targetColor)
                    Painter.GameWorld.LoseLife();
                else
                {
                    scoreSound.Play();
                    Painter.GameWorld.Score += 10;
                }
                Reset();
            }
                
        } else if(Painter.Random.NextDouble() < 0.01)
        {
            velocity = CalculateRandomVelocity();
        }

        if (BoundingBox.Intersects(ball.BoundingBox))
        {
            Color = ball.Color;
            ball.Reset();
        }
    }

    public override void Reset()
    {
        base.Reset();
        Color = CalculateRandomColor();
        velocity = Vector2.Zero;
        position.Y = -origin.Y;
    }

    public void ResetMinSpeed()
    {
        minSpeed = 30;
    }

    Vector2 CalculateRandomVelocity()
    {
        return new Vector2(0f, (float)Painter.Random.NextDouble() * 30 + minSpeed);
    }

    Color CalculateRandomColor()
    {
        int rng = Painter.Random.Next(3);
        if (rng == 0)
            return Color.Red;
        else if (rng == 1)
            return Color.Green;
        else
            return Color.Blue;
    }
}
