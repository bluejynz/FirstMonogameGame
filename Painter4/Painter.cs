using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

class Painter : Game
{
    //variables
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;    
    InputHelper inputHelper;
    static GameWorld gameWorld;

    //constructors
    public Painter()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
        inputHelper = new InputHelper();
        Random = new Random();
    }

    //props
    public static Vector2 ScreenSize { get; private set; }
    public static GameWorld GameWorld { get { return gameWorld; } }
    public static Random Random { get; private set; }

    //methods
    [STAThread]
    static void Main()
    {
        Painter game = new Painter();
        game.Run();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        gameWorld = new GameWorld(Content);
        ScreenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        MediaPlayer.Play(Content.Load<Song>("snd_music"));
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update();
        gameWorld.HandleInput(inputHelper);
        gameWorld.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        gameWorld.Draw(gameTime, spriteBatch);
    }
}