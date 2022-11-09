using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApi.Dto
{
    public class MovieFilterDto
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool InCinemas { get; set; }
        public bool UpcomingRelease { get; set; }
    }
}
