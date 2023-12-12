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
                //return a status with error message
                // return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return new ObjectResult(ex.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
           }

           return cells;
        }
    }
}