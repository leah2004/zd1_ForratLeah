using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace shop_forrat
{
    public partial class Form1 : Form
    {
        private Shop shop;
        private Dictionary<Product, int> cart;


        Playlist playlist = new Playlist();
        Song song = new Song();
        public Form1()
        {
            InitializeComponent();
            shop = new Shop();
            cart = new Dictionary<Product, int>();
        }
 
        private void button1_Click(object sender, EventArgs e)
        {
           
            string name = textBox1.Text;
            decimal price;
            int count = (int)numericUpDown1.Value;

            if (decimal.TryParse(textBox2.Text, out price) && !string.IsNullOrWhiteSpace(name))
            {
                shop.CreateProduct(name, price, count); 
                UpdateProductList();
                textBox1.Clear();
                textBox2.Clear();
                numericUpDown1.Value = 0;
            }
            else
            {
                MessageBox.Show("Введите корректные данные товара.");
            }
        }

        private void UpdateProductList()
        {
            listBox1.Items.Clear();
            foreach (var product in shop.ProductsDictionary())
            {
                listBox1.Items.Add(product.Key.GetInfo() + "; Количество: " + product.Value);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string productName = textBox3.Text;
            int count = (int)numericUpDown2.Value;

            Product product = shop.FindByName(productName);
            if (product != null && shop.ProductsDictionary()[product] >= count)
            {
                if (cart.ContainsKey(product))
                {
                    cart[product] += count;
                }
                else
                {
                    cart[product] = count;
                }
                UpdateCartList();
                shop.Sell(product, count);
                UpdateProductList();
                textBox3.Clear();
                numericUpDown2.Value = 0;
            }
            else
            {
                MessageBox.Show("Товар не найден или недостаточно на складе.");
            }
        }

        private void UpdateCartList()
        {
            listBox2.Items.Clear();
            foreach (var item in cart)
            {
                listBox2.Items.Add(item.Key.GetInfo() + "; Количество: " + item.Value);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var item in cart)
            {
                listBox3.Items.Add($"Продано: {item.Key.GetInfo()}; Количество: {item.Value}");
            }
            cart.Clear();
            UpdateCartList();
            UpdateRevenueLabel();
            MessageBox.Show("Покупка завершена!");
      
        }
        private void UpdateRevenueLabel()
        {
            label4.Text = $"ПРИЫБЫЛЬ МАГАЗИНА - {shop.GetRevenue()} руб.";
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //Добавление новых песен
        private void AddNewSong_Click(object sender, EventArgs e)
        {
            string author, name, filename;
            author = AuthorLine.Text;
            name = NameLine.Text;
            filename = PathLine.Text;
            playlist.AddSong(author, name, filename);
            AuthorLine.ResetText();
            NameLine.ResetText();
            PathLine.ResetText();
            MessageBox.Show("Песня добавлена");
        }
        //Переход по индексу
        private void GoTo_Click(object sender, EventArgs e)
        {
            playlist.GoToSong(Convert.ToInt32(SongIndex.Value));
            UpdateSong();
        }
        //Удаление по значению
        private void DeleteObject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Author.Text) || string.IsNullOrWhiteSpace(SongName.Text) || string.IsNullOrWhiteSpace(FileName.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            // Создаем объект Song на основе введенных данных
            Song songToDelete = new Song(Author.Text, SongName.Text, FileName.Text);

            try
            {
                // Проверяем, существует ли песня в плейлисте
                if (!playlist.Contains(songToDelete))
                {
                    throw new ArgumentException("Песня не найдена в плейлисте.");
                }

                playlist.RemoveSong(songToDelete);
                UpdateSong();
                MessageBox.Show("Песня успешно удалена.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message); // Выводим сообщение об ошибке, если песня не найдена
            }

            /*      playlist.RemoveSong(song);
                  UpdateSong();*/
        }
        //Удаление по индексу
        private void DeleteIndex_Click(object sender, EventArgs e)
        {
            try {
                MessageBox.Show(playlist.RemoveSong(Convert.ToInt32(DelIndex.Value)));
                UpdateSong();
            } catch (ArgumentOutOfRangeException) {
                MessageBox.Show("индекс выходит за границы ");
            }
            
           
        }
        //Переход вперед по плейлисту
        private void NextSong_Click(object sender, EventArgs e)
        {
            playlist.NextSong();
            song = playlist.CurrentSong();
            Author.Text = song.author;
            SongName.Text = song.title;
            FileName.Text = song.filename;
        }
        //Переход назад по плейлисту
        private void BackSong_Click(object sender, EventArgs e)
        {
            playlist.PreviousSong();
            song = playlist.CurrentSong();
            Author.Text = song.author;
            SongName.Text = song.title;
            FileName.Text = song.filename;
        }
        //Очищает плейлист
        private void PlaylistClear_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Вы уверены, что хотите продолжить?", "Подтверждение", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Плейлист удален");
                playlist.Clear();

            }
            else if (result == DialogResult.No)
            {
                MessageBox.Show("Операция отменена");
            }
        }
        //Переход в начало
        private void ToStart_Click(object sender, EventArgs e)
        {
            playlist.GoToStart();
            UpdateSong();
        }
        //Обновляет данные о текущей песне
        private void UpdateSong()
        {
            song = playlist.CurrentSong();
            Author.Text = song.author;
            SongName.Text = song.title;
            FileName.Text = song.filename;
        }

    }
}
