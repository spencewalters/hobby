using System;
using System.Windows.Forms;
using HobbyGame.Programs;

namespace ProvingGrounds.Main {
    public partial class MainMenu : Form {
        public MainMenu() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            GoalsCard card = new GoalsCard();
            card.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e) {
            CrappyStardew stardew = new CrappyStardew();
            stardew.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e) {
            MapMaker form = new MapMaker();
            form.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e) {
            CharacterEditor form = new CharacterEditor();
            form.ShowDialog();
        }
    }
}
