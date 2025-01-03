using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;

namespace BLL.Services
{
    public class DirectorService : ServiceBase, IService<Director, DirectorModel>
    {
        public DirectorService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Director record)
        {
            if (_db.Directors.Any(d => d.Name.ToUpper() == record.Name.ToUpper().Trim() && d.IsRetired == record.IsRetired && 
            d.Surname == record.Surname))
                return Error("Director with the same name, retirement and surname exists!");
            record.Name = record.Name?.Trim();
            _db.Directors.Add(record);
            _db.SaveChanges(); //commit to database
            return Success("Directors created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Directors.SingleOrDefault(d => d.Id == id);
            if (entity is null)
                return Error("Director can't be found");
            if (_db.Movies.Any())
                return Error("Director has relational movies!");
            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return Success("Director deleted successfully.");
        }

        public ServiceBase Update(Director record)
        {
            if (_db.Directors.Any(d => d.Id != record.Id && d.Name.ToUpper() == record.Name.ToUpper().Trim() && d.IsRetired == record.IsRetired &&
                        d.Surname == record.Surname))
                return Error("Director with the same name, retirement and surname exists!");
            record.Name = record.Name?.Trim();
            _db.Directors.Update(record);
            _db.SaveChanges(); //commit to database
            return Success("Directors updated successfully");
        }

        IQueryable<DirectorModel> IService<Director, DirectorModel>.Query()
        {
            return _db.Directors.OrderByDescending(d => d.Name).ThenByDescending(d => d.IsRetired).ThenBy(d => d.Surname).Select(d => new DirectorModel() { Record = d });
        }
    }
}
