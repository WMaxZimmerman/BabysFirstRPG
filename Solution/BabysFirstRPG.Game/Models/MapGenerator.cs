using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Models
{
    public static class MapGenerator
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

                    switch (tile)
                    {
                        case 'c':
                            texture = game.Textures["Corner"];
                            isObstacle = true;
                            break;
                        case 'w':
                            texture = game.Textures["Wall"];
                            isObstacle = true;
                            break;
                        case 'f':
                            texture = game.Textures["Floor"];
                            break;
                        default:
                            texture = null;
                            break;
                    }


                    if (isObstacle)
                    {
                        game.Objects.Add(new Obstacle(texture, new Vector2(posX * _tileSize, posY * _tileSize)));
                    }
                    else
                    {
                        game.Objects.Add(new GameObject(texture, new Vector2(posX * _tileSize, posY * _tileSize)) { Layer = 0 });
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
                new List<char>{'c', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'c' },
                new List<char>{'w', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'w' },
                new List<char>{'w', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'w' },
                new List<char>{'w', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'w' },
                new List<char>{'w', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'w' },
                new List<char>{'w', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'w' },
                new List<char>{'w', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'w' },
                new List<char>{'w', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'w' },
                new List<char>{'c', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'c' }
            };
        }
    }
}
