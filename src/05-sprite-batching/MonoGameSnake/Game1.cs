using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameSnake;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _logo;
    private Sprite _monogameIcon;
    private Sprite _monogameWordmark;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _logo = Content.Load<Texture2D>("images/logo");
        _monogameIcon = new Sprite(_logo, new Rectangle(0, 0, 128, 128));
        _monogameWordmark = new Sprite(_logo, new Rectangle(150, 34, 458, 58));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //  Calculates the center xy-coordinate of the game window
        Vector2 center = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height) * 0.5f;

        _spriteBatch.Begin();
        _monogameIcon.Draw(_spriteBatch, center);
        _monogameWordmark.Draw(_spriteBatch, new Vector2(center.X, center.Y + _monogameIcon.Height));
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
