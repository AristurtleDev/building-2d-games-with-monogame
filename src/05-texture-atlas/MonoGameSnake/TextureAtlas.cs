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

    //  Creates a new TextureAtlas instance with the given Texture2D as the source texture.
    //  The source texture is checked to ensure it is not null and that it was not previously disposed before being
    //  provided to create this TextureAtlas.
    public TextureAtlas(Texture2D texture)
    {
        Debug.Assert(texture is not null);
        Debug.Assert(!texture.IsDisposed);

        _texture = texture;
        _regionLookup = new Dictionary<string, Rectangle>();
    }

    //  Adds a new region to the texture atlas with the specified name at the source rectangle provided.
    //  The source rectangle is a subregion of the texture atlas, so the bounds of the source rectangle must
    //  be contained within the bounds of the texture, otherwise, its invalid.
    public void AddRegion(string name, Rectangle source)
    {
        Debug.Assert(_texture.Bounds.Contains(source));

        _regionLookup.Add(name, source);
    }

    //  Removes the region with the specified name from the texture atlas.
    public void RemoveRegion(string name) => _regionLookup.Remove(name);

    //  Removes all regions from the texture atlas.
    public void RemoveAllRegions() => _regionLookup.Clear();

    //  Creates a new Sprite instance from this texture atlas using the region specified.
    //  A check is made to ensure the region has been defined in this texture atlas.
    public Sprite CreateSprite(string regionName)
    {
        Debug.Assert(_regionLookup.ContainsKey(regionName));

        Rectangle region = _regionLookup[regionName];
        return new Sprite(_texture, region);
    }
}
