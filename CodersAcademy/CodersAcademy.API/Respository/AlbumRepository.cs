using CodersAcademy.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademy.API.Respository
{
    public class AlbumRepository
    {
        private MusicContext Context { get; init; }
        public AlbumRepository(MusicContext context)
        {
            this.Context = context;
        }
        public async Task<IList<Album>> GetAllAsync()
            => await this.Context.Albums.Include(x => x.Musics).ToListAsync();

        public async Task<Album> GetAlbumByIdAsync(Guid id)
            => await this.Context.Albums.Include(x => x.Musics).Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task DeleteAsync(Album model)
        {
            this.Context.Remove(model);
            await this.Context.SaveChangesAsync(); // Commit
;        }

        public async Task CreateAsync(Album album)
        {
            this.Context.AddAsync(album);
            await this.Context.SaveChangesAsync();
        }
    }
}
