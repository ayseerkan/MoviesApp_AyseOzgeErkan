using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class MovieService : ServiceBase, IService<Movie, MovieModel>
    {
        public MovieService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Movie record)
        {
            if (_db.Movies.Any(m => m.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Movies with the same name exists!");
            record.Name = record.Name?.Trim();
            _db.Movies.Add(record);
            _db.SaveChanges(); //commit to database
            return Success("Movies created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Movies.Include(m => m.Director).SingleOrDefault(m => m.Id == id);
            if (entity is null)
                return Error("Movies not found!");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return Success("Movie deleted successfully");
        }

        public IQueryable<MovieModel> Query()
        {
           return _db.Movies.Include(m => m.Director).Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre).OrderByDescending(m => m.Name).Select(m => new MovieModel {Record = m});
        }

        public ServiceBase Update(Movie record)
        {
            if (_db.Movies.Any(m => m.Id != record.Id && m.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Movies with the same name exists!");
            var entity = _db.Movies.SingleOrDefault(m => m.Id == record.Id);
            if (entity == null)
                return Error("Movies not found!");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            entity.Name = record.Name?.Trim();
            entity.Director = record.Director;
            entity.ReleaseDate = record.ReleaseDate;
            entity.TotalRevenue = record.TotalRevenue;
            entity.MovieGenres = record.MovieGenres;
            _db.Movies.Update(entity);
            _db.SaveChanges();
            return Success("Movies updated successfully!");
        }
    }
}
