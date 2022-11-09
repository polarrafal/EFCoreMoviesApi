namespace SharedApi.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public ICollection<CinemaDto> Cinemas { get; set; }
        public ICollection<ActorDto> Actors { get; set; }
    }
}
