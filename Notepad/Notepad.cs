using System;
using System.IO;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Notepad : Form
    {
        private bool isFileSaved = false;
        public Notepad()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isFileSaved == false)
            {
                if (richTextBox1.Text != "")
                {
                    DialogResult result = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Создание файла", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                                richTextBox1.Clear();
                            }
                            catch
                            {
                                MessageBox.Show("Невозможно сохранить файл");
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        richTextBox1.Clear();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            richTextBox1.Clear();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл");
                }
            }
            else { return; }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                    isFileSaved = true;
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить файл");
                }
            }
            else { return; }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDocument1.Print();
                }
                catch
                {
                    MessageBox.Show("Ошибка параметров печати");
                }
            }
            else { return; }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void изменениеЦветаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
            else { return ; }
        }

        private void изToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
            else { return ; }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пользовательский блокнот. Все права защищены.", "О программе");
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void Notepad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isFileSaved == false)
            {
                if (richTextBox1.Text == "")
                {
                    e.Cancel = false;
                }
                else if (richTextBox1.Text != "")
                {
                    DialogResult res = MessageBox.Show("Хотите ли перед выход сохранить изменения?", "Выход", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Yes)
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                                isFileSaved = true;
                            }
                            catch
                            {
                                MessageBox.Show("Невозможно сохранить файл");
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    else if (res == DialogResult.No)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
