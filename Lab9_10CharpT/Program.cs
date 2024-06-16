using System;
using System.Collections;
using System.IO;

public class Program
{
    public static void Main()
    {
        // Виконання завдання 1.1
        string inputFilePath = "input.txt";
        string outputFilePath = "output.txt";
        ReverseNumbersInFile(inputFilePath, outputFilePath);

        // Виконання завдання 2.1
        PrintCharactersAndDigits(inputFilePath);

        // Створення каталогу для завдання 4
        MusicCatalog catalog = new MusicCatalog();

        // Створення дисків та пісень
        MusicDisc disc1 = new MusicDisc("Best Hits");
        Song song1 = new Song("Song 1", "Artist 1", new TimeSpan(0, 3, 45));
        Song song2 = new Song("Song 2", "Artist 2", new TimeSpan(0, 4, 30));

        MusicDisc disc2 = new MusicDisc("Top Tracks");
        Song song3 = new Song("Song 3", "Artist 1", new TimeSpan(0, 5, 0));
        Song song4 = new Song("Song 4", "Artist 3", new TimeSpan(0, 2, 50));

        // Додавання дисків та пісень до каталогу
        catalog.AddDisc(disc1);
        catalog.AddDisc(disc2);

        catalog.AddSongToDisc("Best Hits", song1);
        catalog.AddSongToDisc("Best Hits", song2);

        catalog.AddSongToDisc("Top Tracks", song3);
        catalog.AddSongToDisc("Top Tracks", song4);

        // Перегляд каталогу
        catalog.ViewCatalog();

        // Перегляд окремого диску
        Console.WriteLine("\nViewing disc 'Best Hits':");
        catalog.ViewDisc("Best Hits");

        // Пошук по артисту
        Console.WriteLine("\nSearching for songs by 'Artist 1':");
        catalog.SearchByArtist("Artist 1");

        // Видалення пісні
        catalog.RemoveSongFromDisc("Best Hits", "Song 2");
        Console.WriteLine("\nAfter removing 'Song 2' from 'Best Hits':");
        catalog.ViewDisc("Best Hits");

        // Видалення диску
        catalog.RemoveDisc("Top Tracks");
        Console.WriteLine("\nAfter removing 'Top Tracks':");
        catalog.ViewCatalog();
    }

    // Завдання 1.1: Переписати числа у зворотному порядку з використанням ArrayList та ICloneable
    public static void ReverseNumbersInFile(string inputFilePath, string outputFilePath)
    {
        ArrayList numbers = new ArrayList();

        // Читання чисел з файлу та додавання їх до ArrayList
        using (StreamReader reader = new StreamReader(inputFilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (int.TryParse(line, out int number))
                {
                    numbers.Add(number);
                }
            }
        }

        // Запис чисел у зворотному порядку до іншого файлу
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                writer.WriteLine(numbers[i]);
            }
        }
    }

    // Завдання 2.1: Вивести символи та цифри з використанням ArrayList та IEnumerable
    public static void PrintCharactersAndDigits(string inputFilePath)
    {
        ArrayList nonDigits = new ArrayList();
        ArrayList digits = new ArrayList();

        // Читання символів з файлу та розподіл їх між двома ArrayLists
        using (StreamReader reader = new StreamReader(inputFilePath))
        {
            int character;
            while ((character = reader.Read()) != -1)
            {
                char ch = (char)character;
                if (char.IsDigit(ch))
                {
                    digits.Add(ch);
                }
                else
                {
                    nonDigits.Add(ch);
                }
            }
        }

        // Друк символів, відмінних від цифр
        foreach (char ch in nonDigits)
        {
            Console.Write(ch);
        }

        // Друк цифр
        foreach (char ch in digits)
        {
            Console.Write(ch);
        }

        Console.WriteLine();
    }
}

// Завдання 4: Створення класів для музикального диску та пісні, використання Hashtable
public class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public TimeSpan Duration { get; set; }

    public Song(string title, string artist, TimeSpan duration)
    {
        Title = title;
        Artist = artist;
        Duration = duration;
    }

    public override string ToString()
    {
        return $"{Title} by {Artist}, Duration: {Duration}";
    }
}

public class MusicDisc
{
    public string Title { get; set; }
    public Hashtable Songs { get; private set; }

    public MusicDisc(string title)
    {
        Title = title;
        Songs = new Hashtable();
    }

    public void AddSong(Song song)
    {
        if (!Songs.ContainsKey(song.Title))
        {
            Songs.Add(song.Title, song);
        }
    }

    public void RemoveSong(string songTitle)
    {
        if (Songs.ContainsKey(songTitle))
        {
            Songs.Remove(songTitle);
        }
    }

    public void ViewSongs()
    {
        foreach (DictionaryEntry entry in Songs)
        {
            Console.WriteLine(entry.Value);
        }
    }

    public override string ToString()
    {
        return $"Disc: {Title}, Songs: {Songs.Count}";
    }
}

public class MusicCatalog
{
    private Hashtable discs;

    public MusicCatalog()
    {
        discs = new Hashtable();
    }

    public void AddDisc(MusicDisc disc)
    {
        if (!discs.ContainsKey(disc.Title))
        {
            discs.Add(disc.Title, disc);
        }
    }

    public void RemoveDisc(string discTitle)
    {
        if (discs.ContainsKey(discTitle))
        {
            discs.Remove(discTitle);
        }
    }

    public void AddSongToDisc(string discTitle, Song song)
    {
        if (discs.ContainsKey(discTitle))
        {
            MusicDisc disc = (MusicDisc)discs[discTitle];
            disc.AddSong(song);
        }
    }

    public void RemoveSongFromDisc(string discTitle, string songTitle)
    {
        if (discs.ContainsKey(discTitle))
        {
            MusicDisc disc = (MusicDisc)discs[discTitle];
            disc.RemoveSong(songTitle);
        }
    }

    public void ViewCatalog()
    {
        foreach (DictionaryEntry entry in discs)
        {
            MusicDisc disc = (MusicDisc)entry.Value;
            Console.WriteLine(disc);
            disc.ViewSongs();
        }
    }

    public void ViewDisc(string discTitle)
    {
        if (discs.ContainsKey(discTitle))
        {
            MusicDisc disc = (MusicDisc)discs[discTitle];
            Console.WriteLine(disc);
            disc.ViewSongs();
        }
    }

    public void SearchByArtist(string artist)
    {
        foreach (DictionaryEntry entry in discs)
        {
            MusicDisc disc = (MusicDisc)entry.Value;
            foreach (DictionaryEntry songEntry in disc.Songs)
            {
                Song song = (Song)songEntry.Value;
                if (song.Artist == artist)
                {
                    Console.WriteLine($"Found {song} on disc {disc.Title}");
                }
            }
        }
    }
}
