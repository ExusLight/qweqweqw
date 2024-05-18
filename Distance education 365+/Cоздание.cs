using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Distance_education_365_
{
    public partial class Cоздание : Form
    {
        private List<InteractivePoster> posters = new List<InteractivePoster>();
        private Button btnSelectImage;
        private Button btnSave;
        private TextBox textBoxPosterName;
        private TextBox textBoxThemeTitle;
        private TextBox textBoxThemeDescription;
        private FlowLayoutPanel flowLayoutPanel1;
        private Bitmap selectedImage;
        public Cоздание()
        {
            InitializeComponent();
            InitializeControls();
        }
        private void InitializeControls()
        { // плейхолдеры в тб
            textBoxPosterName = new TextBox() { Text = "Введите название плаката", Location = new Point(10, 10), Width = 200 };
            textBoxThemeTitle = new TextBox() { Text = "Введите название темы", Location = new Point(10, 40), Width = 200 };
            textBoxThemeDescription = new TextBox() { Text = "Введите описание темы", Location = new Point(10, 70), Width = 200 };

            // плейхолдеры в тб
            textBoxPosterName.Enter += RemovePlaceholderText;
            textBoxPosterName.Leave += SetPlaceholderText;
            textBoxThemeTitle.Enter += RemovePlaceholderText;
            textBoxThemeTitle.Leave += SetPlaceholderText;
            textBoxThemeDescription.Enter += RemovePlaceholderText;
            textBoxThemeDescription.Leave += SetPlaceholderText;

            this.Controls.Add(textBoxPosterName);
            this.Controls.Add(textBoxThemeTitle);
            this.Controls.Add(textBoxThemeDescription);

            btnSave = new Button
            {
                Text = "Сохранить",
                Location = new Point(120, 100)
            };
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            btnSelectImage = new Button
            {
                Text = "Выбрать изображение",
                Location = new Point(10, 100)
            };
            btnSelectImage.Click += btnSelectImage_Click;
            this.Controls.Add(btnSelectImage);

            flowLayoutPanel1 = new FlowLayoutPanel
            {
                Location = new Point(10, 130),
                Width = 500,
                Height = 200
            };
            this.Controls.Add(flowLayoutPanel1);
        }

        private void RemovePlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Введите название плаката" || textBox.Text == "Введите название темы" || textBox.Text == "Введите описание темы")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }
        private void SetPlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == textBoxPosterName)
                    textBox.Text = "Введите название плаката";
                else if (textBox == textBoxThemeTitle)
                    textBox.Text = "Введите название темы";
                else if (textBox == textBoxThemeDescription)
                    textBox.Text = "Введите описание темы";

                textBox.ForeColor = Color.Gray;
            }
        }
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                selectedImage = new Bitmap(imagePath);

                PictureBox pictureBox = new PictureBox
                {
                    Image = selectedImage,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 200,
                    Height = 200
                };

                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаем новое объединенное изображение
                Bitmap combinedImage = new Bitmap(800, 400);
                using (Graphics g = Graphics.FromImage(combinedImage))
                {
                    // скриншот
                    if (selectedImage != null)
                    {
                        g.DrawImage(selectedImage, new Rectangle(0, 0, 400, 400));
                    }

                    
                    string posterName = textBoxPosterName.Text; // Название плаката из TextBox
                    Font nameFont = new Font("Arial", 16, FontStyle.Bold);
                    SizeF nameSize = g.MeasureString(posterName, nameFont);
                    PointF nameLocation = new PointF(410, 20);
                    g.DrawString(posterName, nameFont, Brushes.Black, nameLocation);

                    
                    string themeTitle = textBoxThemeTitle.Text; // Название темы из TextBox
                    Font titleFont = new Font("Arial", 16, FontStyle.Bold);
                    SizeF titleSize = g.MeasureString(themeTitle, titleFont);
                    PointF titleLocation = new PointF(410, 60);
                    g.DrawString(themeTitle, titleFont, Brushes.Black, titleLocation);

                    
                    string themeDescription = textBoxThemeDescription.Text; // Описание темы из TextBox
                    Font descriptionFont = new Font("Arial", 12);
                    SizeF descriptionSize = g.MeasureString(themeDescription, descriptionFont);
                    PointF descriptionLocation = new PointF(410, 100);
                    g.DrawString(themeDescription, descriptionFont, Brushes.Black, descriptionLocation);
                }

                // Сохраняем объединенное изображение
                string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Posters");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string fileName = $"{textBoxPosterName.Text}_{DateTime.Now:yyyyMMddHHmmss}.png";
                string filePath = Path.Combine(directory, fileName);
                combinedImage.Save(filePath);

                MessageBox.Show("Плакат сохранен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении плаката: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public class InteractivePoster
        {
            private Bitmap screenshot;

            public InteractivePoster(string imagePath)
            {
                screenshot = new Bitmap(imagePath);
            }

            public bool HasScreenshot { get; internal set; }

            public Bitmap GetScreenshot()
            {
                return (Bitmap)screenshot.Clone();
            }

            public void SaveToFile(string fileName)
            {
                screenshot.Save(fileName);
            }
        }
    }
}





