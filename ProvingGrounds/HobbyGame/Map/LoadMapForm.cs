using System;
using System.Windows.Forms;

namespace HobbyGame.Map {
    public partial class LoadMapForm : Form {
        public string LoadMapText { get; set; }
        public LoadMapForm() {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Yes;
            LoadMapText = mapTextbox.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.No;
        }
    }
}
