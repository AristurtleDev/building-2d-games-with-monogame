using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameSnake;

public class TextureAtlas
{
    //  The reference to the source texture represented by this TextureAtlas.
    private Texture2D _texture;

    //  A dictionary that provides region lookup by name for regions that have been added to this TextureAtlas.
    private Dictionary<string, Rectangle> _regionLookup;

    /// <summary>
    /// Create a new TextureAtlas instance with the given Texture2D as the source texture.
    /// </summary>
    /// <param name="texture">The source texture of the TextureAtlas.</param>
    public TextureAtlas(Texture2D texture)
    {
        Debug.Assert(texture is not null);
        Debug.Assert(!texture.IsDisposed);

        _texture = texture;
        _regionLookup = new Dictionary<string, Rectangle>();
    }

    /// <summary>
    /// Adds a new region to this TextureAtlas with the specified name.
    /// </summary>
    /// <param name="name">The name of the region to add.</param>
    /// <param name="source">The bounds of the region within the TextureAtlas.</param>
    public void AddRegion(string name, Rectangle source)
    {
        Debug.Assert(_texture.Bounds.Contains(source));

        _regionLookup.Add(name, source);
    }

    /// <summary>
    /// Removes the region with the specified name from this TextureAtlas.
    /// </summary>
    /// <param name="name">The name of the region to remove.</param>
    public void RemoveRegion(string name) => _regionLookup.Remove(name);

    /// <summary>
    /// Removes all regions from this TextureAtlas.
    /// </summary>
    public void RemoveAllRegions() => _regionLookup.Clear();

    /// <summary>
    /// Creates a new Sprite instance from this texture atlas using the region specified.
    /// </summary>
    /// <param name="regionName">The name of the region.</param>
    /// <returns>The Sprite created by this method.</returns>
    public Sprite CreateSprite(string regionName)
    {
        Debug.Assert(_regionLookup.ContainsKey(regionName));

        Rectangle region = _regionLookup[regionName];
        return new Sprite(_texture, region);
    }
}
