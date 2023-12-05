using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProteinsController : ControllerBase
    {
        private readonly IProteinDao dao;

        public ProteinsController(IProteinDao dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public ActionResult<IList<Protein>> getProteins()
        {
            IList<Protein> proteins = new List<Protein>();
            try
            {
                foreach (Protein protein in dao.GetProteins())
                {
                    proteins.Add(protein);
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Ok(proteins);
        }

        [HttpGet("/proteins/proteinId={id}")]
        public ActionResult getProteinById(int id)
        {
            Protein protein = new Protein();
            try
            {
                protein = dao.GetProteinById(id);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Ok(protein);
        }

        [HttpGet("/proteins/proteinName={name}")]
        public ActionResult<IList<Protein>> getProteinsBySequenceName(string name)
        {
            IList<Protein> proteins = new List<Protein>();
            try
            {
                foreach (Protein protein in dao.GetProteinsBySequenceName(name))
                {
                    proteins.Add(protein);
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Ok(proteins);
        }

        [HttpGet("/proteins/user={id}")]
        public ActionResult<IList<Protein>> getProteinsByUserId(int id)
        {
            IList<Protein> proteins = new List<Protein>();
            try
            {
                foreach (Protein protein in dao.GetProteinsByUserId(id))
                {
                    proteins.Add(protein);
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Ok(proteins);
        }

        [HttpPost("/proteins/create")]
        public ActionResult createProtein(RegisterProtein proteinParam)
        {
            Protein protein = null;
            try
            {
                protein = dao.CreateProtein(proteinParam.SequenceName, proteinParam.ProteinSequence, proteinParam.Description, proteinParam.FormatType, proteinParam.UserId);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Created($"/proteins/{protein.ProteinId}", protein);
        }

        [HttpPut("/protens/update/{proteinId}")]
        public ActionResult updateProtein(int proteinId, Protein proteinParam)
        {
            Protein protein = null;
            try
            {
                protein = dao.UpdateProtein(proteinParam.ProteinId, proteinParam.SequenceName, proteinParam.ProteinSequence, proteinParam.Description, proteinParam.FormatType, proteinParam.UserId);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Ok(protein);
        }

        [HttpDelete("/proteins/delete/{id}")]
        public ActionResult deleteProtein(int id)
        {
            bool result = false;
            try
            {
                result = dao.DeleteProteinById(id);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            if (result == true)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
