using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DTO;

namespace TimeKeeper.BLL
{
    public class PaginationService<Entity> where Entity:class
    {
        public Tuple<PaginationModel, List<Entity>> CreatePagination(int page, int pageSize, IEnumerable<Entity> data)
        {
            int totalItems = data.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);

            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;

            int currentPage = page - 1;
            var query = data.Skip(currentPage * pageSize).Take(pageSize);

            return new Tuple<PaginationModel, List<Entity>>
                (
                    new PaginationModel
                    {
                        PageSize = pageSize,
                        TotalItems = totalItems,
                        TotalPages = totalPages,
                        Page = page
                    },
                    query.ToList()
                );
        }
    }
}
