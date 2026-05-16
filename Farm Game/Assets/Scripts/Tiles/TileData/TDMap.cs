public class TDMap
{
    public TDTile[,] tiles;
    public int width;
    public int height;
    public TDMap(int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new TDTile[width, height];
    }

    public TDTile GetTile(int x, int y)
    {
        return tiles[x, y];
    }
}
