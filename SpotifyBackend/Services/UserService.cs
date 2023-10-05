using SpotifyBackend.Models;

namespace SpotifyBackend.Services
{
    public class UserService
    {
        public List<SongDTO> Songs { get; set; } = new List<SongDTO>()
        {
            new SongDTO{Id = 0, Name = "$TOOWY",  Autor = "Tyczka"},
            new SongDTO{Id = 1, Name = "Bros Before Hoes",  Autor = "Tyczka"},
            new SongDTO{Id = 2, Name = "Mordo Flex (prod NoOne)",  Autor = "Tyczka"},
            new SongDTO{Id = 3, Name = "Zeus 2",  Autor = "Tyczka"},
            new SongDTO{Id = 4, Name = "Zeus (prod MTC Beatz)",  Autor = "Tyczka"},
        };
        public List<SongDTO> GetRandomSongList()
        {
            List<SongDTO> SongsCopy = new List<SongDTO>(Songs);
            List<SongDTO> randomSongs = new List<SongDTO>();
            for (int i = 1; i <= Songs.Count; i++)
            {
                Random random = new Random();
                SongDTO randomSong = SongsCopy[random.Next(0, SongsCopy.Count)];
                randomSongs.Add(randomSong);
                SongsCopy.Remove(randomSong);
            }
            return randomSongs;
        }
        public List<SongDTO> SearchSongs(string input)
        {
            string searchTerm = input.ToLower();

            List<SongDTO> resultName = Songs.Where(song =>
                song.Name.ToLower().Contains(searchTerm)
            ).ToList();

            List<SongDTO> resultAutor = Songs
                .Where(song => song.Autor.ToLower().Contains(searchTerm))
                .Select(song => new SongDTO { Autor = song.Autor })
                .Select(song => song.Autor)
                .Distinct()
                .Select(autor => new SongDTO { Autor = autor })
                .ToList();

            List<SongDTO> combinedResults = resultAutor
                .Concat(new List<SongDTO> { null })
                .Concat(resultName)
                .ToList();
            return combinedResults;
        }
        public SongDTO? FindSongName(string name)
        {
            return this.Songs.Select(song => new SongDTO()
            {
                Name = song.Name,
                Autor = song.Autor,
                Id = song.Id,
            }).FirstOrDefault(song => song.Name == name);
        }
        public List<SongDTO>? FindSongAutor(string autor)
        {
            var searchTerm = autor.ToLower();
            List<SongDTO> FoundSongs = Songs.Where(song =>
                song.Autor.ToLower().Contains(searchTerm)
            ).ToList();

            return FoundSongs;
        }
    }
}
