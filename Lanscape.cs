using engine.Common;
using engine.Common.Entities;
using engine.Common.Entities3D;
using engine.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace landscape;

public partial class Landscape : Form
{
    public Landscape()
    {
        var width = 1000;
        var height = 1000;
        this.Name = "Landscape";
        this.Text = "Landscape";
        this.Width = width;
        this.Height = height;
        // setting a double buffer eliminates the flicker
        this.DoubleBuffered = true;

        // basic green background
        var background = new Background(width, height) { GroundColor = new RGBA { R = 255, G = 255, B = 255, A = 255 } };
        // put the player in the middle
        var players = new Player[]
          {
                new Player3D() { Name = "YoBro", X = 1000, Y = -200, Z = 1000, ShowDefaultDrawing = true, ShowTarget = true }
          };

        var objects = GenerateLandscape.Generate(10000, 10000);

        var world = new World(
          new WorldConfiguration()
          {
              Width = width,
              Height = height,
              ShowCoordinates = false,
              HorizonX = 1000,
              HorizonY = 1000,
              HorizonZ = 1000,
              Is3D = true,
              EnableZoom = true,
              ForcesApplied = (int)Forces.Y,
          },
          players,
          objects,
          background
        );

        // start the UI painting
        UI = new UIHookup(this, world);

        // hide the cursor
        //System.Windows.Forms.Cursor.Hide();
    }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            UI.ProcessCmdKey(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        } // ProcessCmdKey

        #region private
        private UIHookup UI;
        #endregion
}
