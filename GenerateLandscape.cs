using System;
using System.Collections.Generic;
using engine.Common;
using engine.Common.Entities3D;

namespace landscape
{
    class GenerateLandscape
    {
        // generate a series of Sheet objects that represent a landscape that is width by height with each sheet being 100 by 100
        public static Sheet[] Generate(int width, int depth)
        {
            var sheetWidth = 100;
            var sheetDepth = 100;
            var sheetHeight = 10;

            // create a list of sheets
            var sheets = new List<Sheet>();
            // create a random number generator
            var rand = new Random();
            // create random Y values for the corners
            int numCornersX = width / sheetWidth;
            int numCornersZ = depth / sheetDepth;
            var yValues = new int[numCornersX + 1][];

            // sheet corner layout
            //     -------
            // /\  |\    |
            // .   | \   |
            // .   |  \  |
            // .   |   \ |
            // 0   |    \|
            // z:  -------
            //   x: 0...>

            // generate random Y values for each vertex
            for (int x = 0; x <= numCornersX; x++)
            {
                yValues[x] = new int[numCornersZ + 1];
                for (int z = 0; z <= numCornersZ; z++)
                {
                    yValues[x][z] = rand.Next(-1 * (sheetHeight/2), (sheetHeight/2));
                }
            }

            // loop through the width and height
            // putting down sheets further away and then closer, right to left
            for (var i = 0; i < width / sheetWidth; i++)
            {
                for (var j = 0; j < depth / sheetDepth; j++)
                {
                    // calculate the x and z
                    var x = i * sheetWidth;
                    var z = j * sheetDepth;

                    // average the Y values of the 4 corners
                    var avgY = (yValues[i][j] + yValues[i + 1][j] + yValues[i][j + 1] + yValues[i + 1][j + 1]) / 4;

                    // make a random color
                    var color = new RGBA() { R = (byte)rand.Next(0, 255), G = (byte)rand.Next(0, 255), B = (byte)rand.Next(0, 255), A = 255 };

                    // create a new sheet with the corresponding Y values
                    Sheet sheet = new Sheet()
                    {
                        X = x,
                        Y = avgY,
                        Z = z,
                        Width = sheetWidth,
                        Height = sheetHeight,
                        Depth = sheetDepth,
                        Wireframe = false,
                        DisableShading = false,
                        UniformColor = color,
                        Polygons = new engine.Common.Point[][]
                        {
                                new engine.Common.Point[]
                                {
                                    // (1) away right
                                    new engine.Common.Point() { X = 0.5f, Y = yValues[i + 1][j + 1], Z = 0.5f },
                                    // (2) close right
                                    new engine.Common.Point() { X = 0.5f, Y = yValues[i + 1][j], Z = -0.5f },
                                    // (3) close left
                                    new engine.Common.Point() { X = -0.5f, Y = yValues[i][j], Z = -0.5f }
                                },
                                new engine.Common.Point[]
                                {
                                    // (0) away left
                                    new engine.Common.Point() { X = -0.5f, Y = yValues[i][j + 1], Z = 0.5f },
                                    // (1) away right
                                    new engine.Common.Point() { X = 0.5f, Y = yValues[i + 1][j + 1], Z = 0.5f },
                                    // (3) close left
                                    new engine.Common.Point() { X = -0.5f, Y = yValues[i][j], Z = -0.5f }
                                },
                        }
                    };
                    // add the sheet to the list
                    sheets.Add(sheet);
                }
            }
            // return the list of sheets
            return sheets.ToArray();
        }

    }
}
