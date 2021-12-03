using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameWorld
{
    //variables
    Texture2D background, livesSprite, gameover, scoreBar;
    Cannon cannon;
    Ball ball;
    PaintCan can1, can2, can3;
    int lives;
    SpriteFont gameFont;

    //constructors
    /// <summary>
    /// Esse metodo carrega todos assets relevantes do MonoGame e inicializa todos objetos do jogo:
    /// o canhão, a bola, e as latas de tinta.
    /// Também inicializa todas outras variaveis para que o jogo possa começar.
    /// </summary>
    /// <param name="Content">Um objeto ContentManager, requerido para carregar os assets</param>
    public GameWorld(ContentManager Content)
    {
        background = Content.Load<Texture2D>("spr_background");
        livesSprite = Content.Load<Texture2D>("spr_lives");
        gameover = Content.Load<Texture2D>("spr_gameover");
        scoreBar = Content.Load<Texture2D>("spr_scorebar");
        gameFont = Content.Load<SpriteFont>("PainterFont");
        cannon = new Cannon(Content);
        ball = new Ball(Content);
        can1 = new PaintCan(Content, 480f, Color.Red);
        can2 = new PaintCan(Content, 610f, Color.Green);
        can3 = new PaintCan(Content, 740f, Color.Blue);
        lives = 5;
        Score = 0;
    }

    //props
    public Cannon Cannon { get { return cannon; } }
    public Ball Ball { get { return ball; } }
    public bool IsGameOver { get { return lives <= 0; } }
    public int Score { get; set; }

    //methods
    public void LoseLife()
    {
        lives--;
    }

    void Reset()
    {
        lives = 5;
        Score = 0;
        cannon.Reset();
        ball.Reset();
        can1.Reset();
        can1.ResetMinSpeed();
        can2.Reset();
        can2.ResetMinSpeed();
        can3.Reset();
        can3.ResetMinSpeed();
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (!IsGameOver)
        {
            cannon.HandleInput(inputHelper);
            ball.HandleInput(inputHelper);
        } else if(inputHelper.KeyPressed(Keys.Space))
        {
            Reset();
        }
    }

    public void Update(GameTime gameTime)
    {
        if (IsGameOver)
            return;

        ball.Update(gameTime);
        can1.Update(gameTime);
        can2.Update(gameTime);
        can3.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        spriteBatch.Draw(scoreBar, new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(gameFont, $"Score: {Score}", new Vector2(20, 18), Color.White);

        cannon.Draw(gameTime, spriteBatch);
        ball.Draw(gameTime, spriteBatch);
        can1.Draw(gameTime, spriteBatch);
        can2.Draw(gameTime, spriteBatch);
        can3.Draw(gameTime, spriteBatch);

        for (int i = 0; i < lives; i++)
            spriteBatch.Draw(livesSprite, new Vector2(i * livesSprite.Width + 15, 60), Color.White);

        if (IsGameOver)
        {
            spriteBatch.Draw(gameover, new Vector2(Painter.ScreenSize.X - gameover.Width, Painter.ScreenSize.Y - gameover.Height) / 2, Color.White);
        }

        spriteBatch.End();
    }

    public bool IsOutsideWorld(Vector2 position)
    {
        return position.X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
    }
}
