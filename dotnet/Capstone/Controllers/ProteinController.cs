using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using System.Web;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProteinsController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly IProteinDao proteinDao;
        public ProteinsController(IProteinDao proteinDao, IUserDao userDao)
        {
            this.userDao = userDao;
            this.proteinDao = proteinDao;
        }

        [HttpGet]
        public ActionResult<IList<Protein>> getProteins()
        {
            IList<Protein> proteins = new List<Protein>();
            try
            {
                foreach (Protein protein in proteinDao.GetProteins())
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

        [HttpGet("proteinId={id}")]
        public ActionResult getProteinById(int id)
        {
            Protein protein = new Protein();
            try
            {
                protein = proteinDao.GetProteinById(id);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Ok(protein);
        }

        [HttpGet("proteinName={name}")]
        public ActionResult<IList<Protein>> getProteinsBySequenceName(string name)
        {
            IList<Protein> proteins = new List<Protein>();
            try
            {
                foreach (Protein protein in proteinDao.GetProteinsBySequenceName(name))
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

        [HttpGet("user={id}")]
        public ActionResult<IList<Protein>> getProteinsByUserId(int id)
        {
            IList<Protein> proteins = new List<Protein>();
            try
            {
                foreach (Protein protein in proteinDao.GetProteinsByUserId(id))
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

        [HttpPost]
        public ActionResult createProtein(RegisterProtein proteinParam)
        {
            ReturnUser user = userDao.GetUserByUsername(User.Identity.Name);
            Protein protein = null;
            try
            {
                protein = proteinDao.CreateProtein(proteinParam.SequenceName, proteinParam.ProteinSequence, proteinParam.Description, user.UserId);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Created($"/proteins/{protein.ProteinId}", protein);
        }

        [HttpPut]
        public ActionResult updateProtein(int proteinId, Protein proteinParam)
        {
            ReturnUser user = userDao.GetUserByUsername(User.Identity.Name);
            Protein protein = null;
            try
            {
                protein = proteinDao.UpdateProtein(proteinParam.ProteinId, proteinParam.SequenceName, proteinParam.ProteinSequence, proteinParam.Description, user.UserId);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Ok(protein);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult deleteProtein(int id)
        {
            bool result = false;
            try
            {
                result = proteinDao.DeleteProteinById(id);
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
