using System;
using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
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

        [HttpGet]
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

        [HttpGet("{id}")]
        public ActionResult<List<Cell>> getFastetCells(int id)
        {
           List<Cell> cells = new List<Cell>();
           try
           {
               Protein protein = proteinDao.GetProteinById(id);
               string letters = protein.ProteinSequence;
               List<Cell> cellList = cellDao.getFastestCells(letters);

           } catch (DaoException ex)
           {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
           }

           return cells;
        }

        [HttpPatch("{id}")]
        public ActionResult<Protein> updateProteinSequence(int id)
        {
            Protein protein = null;
            try
            {
                protein = proteinDao.GetProteinById(id);

                string letters = protein.ProteinSequence;
                List<Cell> cellList = cellDao.getFastestCells(letters);

                Cell cell_1 = cellList[0];
                Cell cell_2 = cellList[1];
                Cell cell_3 = cellList[2];

                protein.Sequence1 = cell_name(protein.ProteinSequence, cell_1);
                protein.Sequence2 = cell_name(protein.ProteinSequence, cell_2);
                protein.Sequence3 = cell_name(protein.ProteinSequence, cell_3);

                proteinDao.OptimizeProtein(protein);
            }
            catch (DaoException ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return protein;
        }

        private string cell_name(String sequince, Cell cell)
        {
          string first = cell.LetterY;
          string second = cell.LetterX;
          string sub_str = sequince.Substring(2);

          return first + second + sub_str;
        }
    }
}
