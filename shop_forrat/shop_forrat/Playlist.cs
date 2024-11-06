using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop_forrat
{
    internal class Playlist
    {

        private List<Song> list;
        private int currentIndex;

        // Конструктор
        public Playlist()
        {
            list = new List<Song>();
            currentIndex = 0;
        }
        // Метод для проверки наличия песни в плейлисте
        public bool Contains(Song song)
        {
            return list.Any(s => s.Equals(song)); // Используем метод Equals для сравнения
        }

        // Метод для получения текущей аудиозаписи
        public Song CurrentSong()
        {
            if (list.Count > 0)
                return list[currentIndex];
            else
                throw new IndexOutOfRangeException("Невозможно получить текущую аудиозапись для пустого плейлиста!");
        }

        // Метод для добавления аудиозаписи
        public void AddSong(Song song)
        {
            list.Add(song);
        }

        // Перегрузка AddSong
        public void AddSong(string author, string title, string filename)
        {
            Song song = new Song(author, title, filename);
            list.Add(song);
        }

        // Переход к следующей песне
        public void NextSong()
        {
            if (list.Count > 0)
            {
                currentIndex = (currentIndex + 1) % list.Count;
            }
        }

        // Переход к предыдущей песне
        public void PreviousSong()
        {
            if (list.Count > 0)
            {
                currentIndex = (currentIndex - 1 + list.Count) % list.Count;
            }
        }

        // Переход по индексу записи
        public void GoToSong(int index)
        {
            if (index >= 0 && index < list.Count)
            {
                currentIndex = index;
            }
            else
            {
                throw new IndexOutOfRangeException("Индекс вне диапазона!");
            }
        }

        // Переход к началу списка
        public void GoToStart()
        {
            currentIndex = 0;
        }

        // Удаление композиции по индексу
        public string RemoveSong(int index)
        {
            try
            {
                list.RemoveAt(index);
                if (currentIndex >= list.Count) currentIndex = list.Count - 1; // Обновляем индекс
                return "Композиция успешно удалена";
            }
            catch { return "Ошибка"; }
        }

        // Удаление композиции по значению
        public void RemoveSong(Song song)
        {
            int index = list.IndexOf(song);
            if (index != -1)
            {
                RemoveSong(index);
            }
        }

        // Очистка плейлиста
        public void Clear()
        {
            list.Clear();
            currentIndex = 0;
        }

    }
}
