using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
<<<<<<< HEAD
using System.Web;
using System.Net.Http;
using System;

=======

>>>>>>> 72058b23ca7d8f15af4a0c94d4a663eccfd3ba30
namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProteinsController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly IProteinDao proteinDao;
        private readonly ICellDao cellDao;

        public ProteinsController(IProteinDao proteinDao, IUserDao userDao, ICellDao cellDao)
        {
            this.userDao = userDao;
            this.proteinDao = proteinDao;
            this.cellDao = cellDao;
        }

        [HttpGet]
        public ActionResult<IList<Protein>> GetProteins()
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
                return StatusCode(500, "An internal server error occurred.");
            }
            return Ok(proteins);
        }

        [HttpGet("proteinId={id}")]
        public ActionResult GetProteinById(int id)
        {
            Protein protein = new Protein();
            try
            {
                protein = proteinDao.GetProteinById(id);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
            return Ok(protein);
        }

        [HttpGet("proteinName={name}")]
        public ActionResult<IList<Protein>> GetProteinsBySequenceName(string name)
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
                return StatusCode(500, "An internal server error occurred.");
            }

            return Ok(proteins);
        }

        [HttpGet("user={username}")]
        public ActionResult<IList<Protein>> GetProteinsByUsername(string username)
        {
            IList<Protein> proteins = new List<Protein>();
            try
            {
                foreach (Protein protein in proteinDao.GetProteinsByUsername(username))
                {
                    proteins.Add(protein);
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
            return Ok(proteins);
        }

        [HttpPost]
        public ActionResult CreateProtein(RegisterProtein proteinParam)
        {
            ReturnUser user = userDao.GetUserByUsername(User.Identity.Name);
            Protein protein = null;
            try
            {
                protein = proteinDao.CreateProtein(proteinParam.SequenceName, proteinParam.ProteinSequence, proteinParam.Description, user.Username, user.UserId);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
            return Created($"/proteins/{protein.ProteinId}", protein);
        }

        [HttpPut]
        public ActionResult UpdateProtein(int proteinId, Protein proteinParam)
        {
            ReturnUser user = userDao.GetUserByUsername(User.Identity.Name);
            Protein protein = null;
            try
            {
                protein = proteinDao.UpdateProtein(proteinParam.ProteinId, proteinParam.SequenceName,
                    proteinParam.ProteinSequence, proteinParam.Description, user.UserId);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occurred.");
            }

            return Ok(protein);
<<<<<<< HEAD
        }

=======
        }

>>>>>>> 72058b23ca7d8f15af4a0c94d4a663eccfd3ba30
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteProtein(int id)
        {
            bool result = false;
            try
            {
                result = proteinDao.DeleteProteinById(id);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
            if (result == true)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpGet("api/ncbi/{name}")]
        public ActionResult<Protein> GetApiProtein(string name)
        {
            Protein protein = new Protein();
            try
            {
                string id = proteinDao.ApiGetProteinId(name).Result;
                protein = proteinDao.ApiGetProteinSequence(id).Result;
                protein.SequenceName = name;
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
            return Ok(protein);
        }
    }
}













