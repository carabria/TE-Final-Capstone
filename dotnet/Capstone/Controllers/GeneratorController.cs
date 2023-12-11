using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class GeneratorController
    {
        private readonly ICellDao cellDao;
        private readonly IProteinDao proteinDao;
        
        public GeneratorController(ICellDao cellDao, IProteinDao proteinDao)
        {
            this.cellDao = cellDao;
            this.proteinDao = proteinDao;
        }

        public ActionResult<List<Cell>> getCells()
        {
            List<Cell> cells = new List<Cell>();
            try
            {
                foreach (Cell cell in cellDao.getCells())
                {
                    cells.Add(cell);
                }
            }
            catch (DaoException)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return cells;
        }
    }
}