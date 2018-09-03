using System.Collections.Generic;
using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Models
{
    public class MapGenerator
    {
        private static int _tileSize = 32;

        public static void GenerateMap(MainGame game)
        {
            var map = LevelOne();
            var posY = 0;

            foreach (var row in map)
            {
                var posX = 0;

                foreach (var tile in row)
                {
                    Texture2D texture;
                    var isObstacle = false;
                    var orientation = 0;

                    switch (tile)
                    {
                        case 'r':
                            texture = game.Textures["Wall"];
                            isObstacle = true;
                            break;
                        case 't':
                            texture = game.Textures["Wall"];
                            isObstacle = true;
                            orientation = 90;
                            break;
                        case 'l':
                            texture = game.Textures["Wall"];
                            isObstacle = true;
                            orientation = 180;
                            break;
                        case 'b':
                            texture = game.Textures["Wall"];
                            isObstacle = true;
                            orientation = 270;
                            break;
                        case 'R':
                            texture = game.Textures["Corner"];
                            isObstacle = true;
                            break;
                        case 'T':
                            texture = game.Textures["Corner"];
                            isObstacle = true;
                            orientation = 90;
                            break;
                        case 'L':
                            texture = game.Textures["Corner"];
                            isObstacle = true;
                            orientation = 180;
                            break;
                        case 'B':
                            texture = game.Textures["Corner"];
                            isObstacle = true;
                            orientation = 270;
                            break;
                        case 'f':
                            texture = game.Textures["Floor"];
                            break;
                        case 'g':
                            texture = game.Textures["WallTransitionRight"];
                            isObstacle = true;
                            break;
                        case 'p':
                            texture = game.Textures["WallTransitionLeft"];
                            isObstacle = true;
                            break;
                        case 'h':
                            texture = game.Textures["FloorTransition"];
                            break;
                        case 'G':
                            texture = game.Textures["WallTransitionRight"];
                            isObstacle = true;
                            orientation = 90;
                            break;
                        case 'P':
                            texture = game.Textures["WallTransitionLeft"];
                            isObstacle = true;
                            orientation = 90;
                            break;
                        case 'H':
                            texture = game.Textures["FloorTransition"];
                            orientation = 90;
                            break;
                        default:
                            texture = null;
                            break;
                    }


                    if (isObstacle)
                    {
                        game.Objects.Add(new Obstacle(texture, new Vector2(posX * _tileSize, posY * _tileSize)){ OrientationDegree = orientation});
                    }
                    else
                    {
                        game.Objects.Add(new GameObject(texture, new Vector2(posX * _tileSize, posY * _tileSize)) { Layer = 0, OrientationDegree = orientation });
                    }

                    posX++;
                }

                posY++;
            }
        }

        public static List<List<char>> LevelOne()
        {
            return new List<List<char>>
            {
                new List<char>{'R', 'g', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'P', 'T' },
                new List<char>{'p', 'h', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'H', 'G' },
                new List<char>{'l', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'r' },
                new List<char>{'l', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'r' },
                new List<char>{'l', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'r' },
                new List<char>{'l', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'r' },
                new List<char>{'l', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'r' },
                new List<char>{ 'l', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'r' },
                new List<char>{'B', 't', 't', 't', 't', 't', 't', 't', 't', 't', 'L' }
            };
        }
    }
}
