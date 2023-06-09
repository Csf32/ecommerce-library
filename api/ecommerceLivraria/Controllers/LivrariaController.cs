using System.Net;
using ecommerceLivraria.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;


namespace ecommerceLivraria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrariaController : ControllerBase
    {
       private readonly ToDoContextModel _context; 

       public LivrariaController(ToDoContextModel context)
       {
        
           _context = context;

//            _context = new ToDoContextModel(options);
// _context.Database.EnsureDeleted();
// _context.Database.EnsureCreated();

            _context.produtos.Add(new ProdutosModel{ Id = "2", Nome = "Book 1", Preco = 25.0, Quant = 11, Categoria = "Auto-Ajuda", Imagem = "img1"});
            _context.produtos.Add(new ProdutosModel{ Id = "3", Nome = "Book 2", Preco = 15.0, Quant = 7, Categoria = "Romance", Imagem = "img2"});
            _context.produtos.Add(new ProdutosModel{ Id = "4", Nome = "Book 3", Preco = 17.0, Quant = 8, Categoria = "Ficção Científica", Imagem = "img3"});
            _context.produtos.Add(new ProdutosModel{ Id = "5", Nome = "Book 4", Preco = 29.0, Quant = 21, Categoria = "Fantasia", Imagem = "img4"});
            _context.produtos.Add(new ProdutosModel{ Id = "6", Nome = "Book 5", Preco = 45.0, Quant = 14, Categoria = "Tecnologia", Imagem = "img5"});
            
            _context.SaveChanges();
          
            
       }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<ProdutosModel>>> getProdutos()
       {
            return await _context.produtos.ToListAsync();
       }

      [HttpGet("{id}")]
      public async Task<ActionResult<ProdutosModel>> getItem(int id)
      {
          
          try{
               var item = await _context.produtos.FindAsync(id.ToString());

               if(item == null)
               {
                    return NotFound($"O produto com o ID {id} não foi encontrado");
               }
          return item;
          }

          catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, e);
          }
          
      }

      [HttpPost("/create")]
      public async Task<ActionResult<ProdutosModel>> createProdutos(ProdutosModel produtosModel)
      {
          await _context.produtos.AddAsync(produtosModel);
          await _context.SaveChangesAsync();
          return produtosModel;

      }
     

     }
}