using GoF.FacadePattern.Facade;
using GoF.FacadePattern.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoF.FacadePattern.Controllers
{
    /// <summary>
    /// ���i���Ɋւ���API�R���g���[���[�B
    /// ���i�̎擾��ꗗ��񋟂��܂��B
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsFacade _productsFacade;

        /// <summary>
        /// ProductsController�̃R���X�g���N�^�[�B
        /// </summary>
        /// <param name="productsFacade">���i�̎擾��Ǘ����s���t�@�T�[�h�C���^�[�t�F�[�X�B</param>
        public ProductsController(IProductsFacade productsFacade)
        {
            _productsFacade = productsFacade;
        }

        /// <summary>
        /// �w�肳�ꂽID�̏��i���擾���܂��B
        /// </summary>
        /// <param name="id">�擾���鏤�i��ID�B</param>
        /// <returns>�w�肳�ꂽ���i�̏��B������Ȃ��ꍇ��404��Ԃ��܂��B</returns>
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
        /// ���ׂĂ̏��i���擾���܂��B
        /// </summary>
        /// <returns>���i�̃��X�g��Ԃ��܂��B</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productsFacade.GetAllProductsAsync();
            return Ok(products);
        }
    }
}
