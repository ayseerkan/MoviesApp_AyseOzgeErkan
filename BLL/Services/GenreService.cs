using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GenreService : ServiceBase, IService<Genre, GenreModel>
    {
        public GenreService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Genre record)
        {
            if (_db.Genres.Any(g => g.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists!");
            record.Name = record.Name.Trim();
            _db.Add(record);
            _db.SaveChanges();
            return Success("Genre created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            Genre entity = _db.Genres.Include(g => g.MovieGenres).SingleOrDefault(g => g.Id == id);
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            _db.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully.");
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.OrderBy(g => g.Name).Select(g => new GenreModel() { Record = g }); //Don't forget Record = g !!!!!
        }

        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(g => g.Id != record.Id && g.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists!");
            Genre entity = _db.Genres.SingleOrDefault(g => g.Id == record.Id);
            entity.Name = record.Name.Trim();
            _db.Update(entity);
            _db.SaveChanges();
            return Success("Genre updated successfully.");
        }
    }
}
