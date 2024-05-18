using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Distance_education_365_
{
    public partial class Form2 : Form
    {


        public Form2()
        {
            InitializeComponent();
            LoadPosters();
        }
        private void LoadPosters()
        {
            string postersDirectory = Path.Combine(Application.StartupPath, "Posters");
            if (!Directory.Exists(postersDirectory))
            {
                MessageBox.Show("Отсутствуют плакаты в папке 'Posters'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] posterFiles = Directory.GetFiles(postersDirectory, "*.png");
            foreach (string posterFile in posterFiles)
            {
                string posterName = Path.GetFileNameWithoutExtension(posterFile);
                comboBox1.Items.Add(posterName);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            {
                string selectedPosterName = comboBox1.SelectedItem as string;
                if (string.IsNullOrEmpty(selectedPosterName))
                {
                    MessageBox.Show("Выберите плакат для открытия.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string posterPath = Path.Combine(Application.StartupPath, "Posters", selectedPosterName + ".png");
                if (!File.Exists(posterPath))
                {
                    MessageBox.Show("Выбранный плакат не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Открываем плакат
                try
                {
                    Process.Start(posterPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось открыть плакат: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}