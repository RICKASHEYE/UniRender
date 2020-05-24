using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
{
    public class TileManager
    {
        //The tile renderer on the screen
        public List<Tile> tiles = new List<Tile>();

        public TileManager()
        {
            Debug.Log("Started the game with tiles lmao");
        }

        public void CreateTile(Tile tile)
        {
            if(tile == null)
            {
                Debug.Error("A tile needs to be specified");
                return;
            }

            if(tile.tilePos == Vector2.zero)
            {
                Debug.Error("The tile specified has a position missing or is zero");
            }

            if(tile.tileImage == null)
            {
                Debug.Error("Unable to render, as tile image is missing");
                return;
            }

            Tile tileGet = getTile(tile.name);
            if(tileGet != null)
            {
                Debug.Error("Tile with the name already exists please choose another name :(");
            }

            foreach(Tile til in tiles)
            {
                if(til.tilePos == tile.tilePos)
                {
                    Debug.Error("There is a tile in the same position not rendering...");
                    return;
                }
            }

            tiles.Add(tile);
        }

        public void RemoveTile(string tileName)
        {
            if (getTile(tileName) != null)
            {
                tiles.Remove(getTile(tileName));
            }
            else
            {
                Debug.Error("Tile doesnt exist!");
            }
        }

        public void MoveTile(string tileName, Vector2 newPosition)
        {
            Tile tile = getTile(tileName);
            tile.tilePos = newPosition;
        }

        public void ChangeImageTile(string tileName, ParEngineImage image)
        {
            if (image != null)
            {
                Tile tile = getTile(tileName);
                tile.tileImage = image;
            }
            else
            {
                Debug.Error("The image given is null!");
            }
        }

        public void ChangeTileName(string tileNameOriginal, string tileNameNew)
        {
            if (tileNameNew != " ")
            {
                Tile tile = getTile(tileNameOriginal);
                tile.name = tileNameNew;
            }
            else
            {
                Debug.Error("The new tile name is not valid");
            }
        }

        public Tile getTile(string tileName)
        {
            Tile returnedTile = null;
            foreach(Tile tile in tiles)
            {
                if(tile.name == tileName)
                {
                    returnedTile = tile;
                }
            }
            return returnedTile;
        }

        /*public void DrawTiles()
        {
            foreach(Tile tile in tiles)
            {
                Canvas.DrawImage(new Rectangle(new Vector2(16, 16), tile.tilePos), tile.tileImage, "Tile" + tile.tilePos.x + "." + tile.tilePos.y);
            }
        }*/
    }
}
