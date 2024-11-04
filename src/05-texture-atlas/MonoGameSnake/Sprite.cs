using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameSnake;

public class Sprite
{
    //  The reference to the source texture used when rendering this sprite.
    private readonly Texture2D _texture;

    //  The source rectangle that represents the region in the source texture to use when rendering this sprite.
    private readonly Rectangle _sourceRectangle;

    //  The color tint to apply when rendering this sprite.
    //  Default value is Color.White.
    public Color Color { get; set; } = Color.White;

    //  The amount of rotation, in radians, to apply when rendering this sprite.
    //  Default value is no rotation, or 0.0f.
    public float Rotation { get; set; } = 0.0f;

    //  The scale factor to apply to the x- and y-axes when rendering this sprite.
    //  Default value is a scale of 1 on the x- and y-axes, or Vector2.Zero.
    public Vector2 Scale { get; set; } = Vector2.One;

    //  The xy-coordinate origin point to use when rendering this sprite.
    //  Affects the offset of the texture when rendered as well as being the origin in which the sprite is
    //  rotated and scaled from.
    //  Default value is top-left origin, or Vector2.Zero.
    public Vector2 Origin { get; set; } = Vector2.Zero;

    //  The SpriteEffect value that specifies if the sprite should be flipped horizontally, vertically, or both
    //  when rendered.
    //  Default value is no horizontal or vertical flipping, or SpriteEffects.None.
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    //  The depth at which the sprite is rendered.
    //  Default value is 0.0f.
    public float LayerDepth { get; set; } = 0.0f;

    //  The width of this sprite, derived from the width of the source rectangle multiplied by the scale factor
    //  of the x-axis.
    public float Width => _sourceRectangle.Width * Scale.X;

    //  The height of this sprite, derived from the height of the source rectangle multiplied by the scale factor
    //  of the y-axis.
    public float Height => _sourceRectangle.Height * Scale.Y;

    //  Creates a new Sprite instance using the source texture and source rectangle provided.
    //  The source texture is checked to ensure it is not null and that it was not previously disposed.
    public Sprite(Texture2D texture, Rectangle sourceRectangle)
    {
        Debug.Assert(texture is not null);
        Debug.Assert(!texture.IsDisposed);

        _texture = texture;
        _sourceRectangle = sourceRectangle;
    }

    //  Draws this sprite using the SpriteBatch given at the position specified.
    //  Other parameters for the SpriteBatch.Draw method call are supplied via tha properties of this Sprite.
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(_texture, position, _sourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
}
