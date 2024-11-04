using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameSnake;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _logo;
    private TextureAtlas _textureAtlas;
    private Sprite _bodySprite;
    private Sprite _foodSprite;

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

        Texture2D atlasTexture = Content.Load<Texture2D>("images/texture-atlas");
        _textureAtlas = new TextureAtlas(atlasTexture);
        _textureAtlas.AddRegion("body", new Rectangle(0, 0, 32, 32));
        _textureAtlas.AddRegion("food", new Rectangle(32, 0, 32, 32));
        _textureAtlas.AddRegion("arrow", new Rectangle(0, 32, 32, 32));
        _textureAtlas.AddRegion("grid", new Rectangle(32, 32, 32,32));
        _textureAtlas.AddRegion("nineslice", new Rectangle(0, 64, 32, 32));

        _bodySprite = _textureAtlas.CreateSprite("body");
        _foodSprite = _textureAtlas.CreateSprite("food");

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

        Rectangle iconSourceRect = new Rectangle(0, 0, 128, 128);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_logo,
            new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height) * 0.5f,
            iconSourceRect,
            Color.White,
            0.0f,
            new Vector2(iconSourceRect.Width, iconSourceRect.Height) * 0.5f,
            1.0f,
            SpriteEffects.None,
            0.0f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
