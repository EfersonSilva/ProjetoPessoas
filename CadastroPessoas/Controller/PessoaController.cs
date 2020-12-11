using Microsoft.AspNetCore.Mvc;
using ProjetoPloomers.BLL;
using ProjetoPloomers.Models;

namespace ProjetoPloomers.Controller
{
    [Route("api/")]
    [ApiController]
    public class PessoaController : ControllerBase
    {

        [HttpPost, Route("Insert/")]
        public bool Insert(Pessoa pessoa)
        {
            var result = false;

            result = new PessoaBLL().Insert(pessoa);

            return result;
        }

        [HttpPost, Route("Update/")]
        public Pessoa Update(Pessoa pessoa)
        {
            var result = new PessoaBLL().Update(pessoa);

            return result;
        }

        [HttpPost, Route("Search/")]
        public Pessoa Search(Pessoa pessoa)
        {
            var result = new PessoaBLL().Search(pessoa);
            return result;
        }

        [HttpDelete, Route("Delete/")]
        public bool Delete(Pessoa pessoa)
        {
            var result = new PessoaBLL().Delete(pessoa);

            return result;
        }
    }
}