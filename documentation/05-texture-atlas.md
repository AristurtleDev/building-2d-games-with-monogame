# Chapter 05: Texture Atlas

In this chapter, we're going to take the source rectangle concept learned in [Chapter 04](04-working-with-textures.md#drawing-texture-regions) and create a Texture Atlas class that we can use to create sprites.

When rendering graphic in MonoGame using the `SpriteBatch`, you want to batch as many draw calls as possible.  However, there's more to batching than just putting all your draw calls between a `SpiteBatch.Begin` and `SpriteBatch.End` block.  Every time you make a call to `SpriteBatch.Draw`, if the *texture* parameter used is a different texture than the one used in the previous draw, then the `SpriteBatch` has to perform a texture swap on the GPU.   For instance, let's look at the following example that you might see in a Pong type game:

```cs
private Texture2D _ball;
private Texture2D _paddle;
private Vector2 _leftPaddlePosition;
private Vector2 _rightPaddlePosition;
private Vector2 _ballPosition;

protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin();
    _spriteBatch.Draw(_paddle, _leftPaddlePosition, Color.White);
    _spriteBatch.Draw(_ball, _ballPosition, Color.Red);
    _spriteBatch.Draw(_paddle, _rightPaddlePosition, Color.White);
    _spriteBatch.End();
}
```

Once the sprite batch has begun, the `Draw` method calls perform the following in order

1. `_paddle` texture is drawn,
2. `_ball` texture is drawn, which is a different texture than the previous, so a texture swap occurs.
3. `_paddle` texture is drawn, which is a different texture than the previous, so a texture swap occur.

Because the draw order is paddle, ball, paddle, we've introduced two texture swaps.  In a very minimal example such as this, the texture swaps aren't going to make any noticeable difference. However, imagine a full game with hundreds of sprites being drawn at once.  Texture swapping that often is going to lead to poor draw performance in your game.

You may be thinking that a clever solution could be to change the draw order so you draw both paddles first and then the ball.  For example:

```cs
_spriteBatch.Draw(_paddle, _leftPaddlePosition, Color.White);
_spriteBatch.Draw(_paddle, _rightPaddlePosition, Color.White);
_spriteBatch.Draw(_ball, _ballPosition, Color.Red);
```

And, in fact, this would reduce the texture swapping down to only one in this scenario.  Again though, this is a very minimal example, and you can't rely on draw order always being in your favor when you have hundreds of draw calls happening.

A better solution to this is to use a *Texture Atlas*.

## What is a Texture Atlas
