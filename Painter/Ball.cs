using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Ball : ThreeColorGameObject
{
    //variables
    bool shooting;
    SoundEffect shootSound;

    //constructors
    public Ball(ContentManager Content) : base(Content, "spr_ball_red", "spr_ball_green", "spr_ball_blue")
    {
        shootSound = Content.Load<SoundEffect>("snd_shoot_paint");
    }

    //methods
    public override void Reset()
    {
        base.Reset();
        shooting = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed() && !shooting)
        {
            shooting = true;
            shootSound.Play();
            velocity = (inputHelper.MousePosition - Painter.GameWorld.Cannon.Position) * 1.2f;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (shooting)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity.Y += 400f * dt;
        } else
        {
            Color = Painter.GameWorld.Cannon.Color;
            position = Painter.GameWorld.Cannon.BallPosition;
        }
        if (Painter.GameWorld.IsOutsideWorld(position))
            Reset();
    }
}
