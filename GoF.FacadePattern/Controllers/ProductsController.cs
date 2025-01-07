using GoF.FacadePattern.Facade;
using GoF.FacadePattern.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoF.FacadePattern.Controllers
{
    /// <summary>
    /// 商品情報に関するAPIコントローラー。
    /// 商品の取得や一覧を提供します。
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsFacade _productsFacade;

        /// <summary>
        /// ProductsControllerのコンストラクター。
        /// </summary>
        /// <param name="productsFacade">商品の取得や管理を行うファサードインターフェース。</param>
        public ProductsController(IProductsFacade productsFacade)
        {
            _productsFacade = productsFacade;
        }

        /// <summary>
        /// 指定されたIDの商品を取得します。
        /// </summary>
        /// <param name="id">取得する商品のID。</param>
        /// <returns>指定された商品の情報。見つからない場合は404を返します。</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productsFacade.GetProductByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// すべての商品を取得します。
        /// </summary>
        /// <returns>商品のリストを返します。</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productsFacade.GetAllProductsAsync();
            return Ok(products);
        }
    }
}
