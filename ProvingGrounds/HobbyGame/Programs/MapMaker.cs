using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HobbyGame;
using System.IO;

namespace HobbyGame.Programs {
    public partial class MapMaker : Form {
        private MapMakerModel mapmaker;

        public MapMaker() {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            mapmaker = new MapMakerModel(tilesPanel, mapPanel);
        }

        private void MapMaker_Load(object sender, EventArgs e) {
        }

        private void saveButton_Click(object sender, EventArgs e) {

        }

        private void tilesPanel_Paint(object sender, PaintEventArgs e) {
            mapmaker.DrawTiles();
        }

        private void tilesPanel_Resize(object sender, EventArgs e) {
            mapmaker.DrawTiles();
        }

        private void tilesPanel_MouseClick(object sender, MouseEventArgs e) {
            mapmaker.TileClicked(e);
        }

        private void mapPanel_MouseClick(object sender, MouseEventArgs e) {
            mapmaker.MapClicked(e);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
            if (mapmaker.MapLoaded && mapmaker.ChangesMade)
                if (MessageBox.Show("Cancel changes?", "Changes Made", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            MakeNewMap();
        }

        private void MakeNewMap() {
            if (mapmaker.MapLoaded && mapmaker.ChangesMade)
                if (MessageBox.Show("Cancel changes?", "Changes Made", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

            mapmaker.NewMap();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "Map Files (*.map)|*.map";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                mapmaker.Open( openFileDialog1.FileName );
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((mapmaker.ChangesMade) && (mapmaker.MapLoaded))
                mapmaker.Save();
        }

        private void tilesSelectionCombo_SelectedIndexChanged(object sender, EventArgs e) {
            string tileSetSelected = tilesSelectionCombo.SelectedItem.ToString();
            mapmaker.SetTileSet(tileSetSelected);
        }
    }
}
