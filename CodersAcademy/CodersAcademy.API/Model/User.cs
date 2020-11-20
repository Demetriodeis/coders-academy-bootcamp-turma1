using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademy.API.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Photo { get; set; }
        public IList<UserFavoriteMusic> FavoriteMusics { get; set; }
        public void AddFavoriteMusic(Music music)
        {
            this.FavoriteMusics.Add(new UserFavoriteMusic()
            {
                Music = music,
                MusicID = music.Id,
                User = this, 
                UserID = this.Id
            });
        }
        public void RemoveFavoriteMusic(Music music)
        {
            var favMusic = this.FavoriteMusics.Where(x => x.MusicID == music.Id).FirstOrDefault();

            if (favMusic == null)
                throw new Exception("Não encontrada a musica na lista de favoritos");

            this.FavoriteMusics.Remove(favMusic);
        }
    }
}
