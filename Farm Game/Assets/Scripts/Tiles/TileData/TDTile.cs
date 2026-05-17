public class TDTile
{
    public const int TILE_EMPTY = 0;
    public const int TILE_FARMLAND = 1;
    public int type = TILE_EMPTY;
    public float posX, posY, posZ;
    public bool obstacle = false;

    public bool edgeNorth, edgrSouth, edgeEast, edgeWest;
    public bool midle, center, full;
}
