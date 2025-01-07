using GoF.FacadePattern.External;
using GoF.FacadePattern.Models;
using GoF.FacadePattern.Repositories;

namespace GoF.FacadePattern.Facade
{
    /// <summary>
    /// 商品データにアクセスするためのファサードインターフェース。
    /// データベースや外部サービスを統一的に扱う機能を提供します。
    /// </summary>
    public interface IProductsFacade
    {
        /// <summary>
        /// 指定されたIDの商品を取得します。
        /// </summary>
        /// <param name="id">取得する商品のID。</param>
        /// <returns>指定された商品のデータ。見つからない場合はnull。</returns>
        Task<Product?> GetProductByIdAsync(int id);

        /// <summary>
        /// すべての商品を取得します。
        /// </summary>
        /// <returns>すべての商品のリスト。</returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }

    /// <summary>
    /// 商品データにアクセスするためのファサード実装。
    /// データベースと外部サービスを利用して商品情報を取得します。
    /// </summary>
    public class ProductsFacade : IProductsFacade
    {
        private readonly IProductRepository _productRepository;
        private readonly IExternalProductService _externalProductService;

        /// <summary>
        /// ProductsFacadeのコンストラクター。
        /// </summary>
        /// <param name="productRepository">商品情報を管理するリポジトリ。</param>
        /// <param name="externalProductService">外部サービスから商品情報を取得するクラス。</param>
        public ProductsFacade(IProductRepository productRepository, IExternalProductService externalProductService)
        {
            _productRepository = productRepository;
            _externalProductService = externalProductService;
        }

        /// <summary>
        /// 指定されたIDの商品を取得します。
        /// データベースをまず確認し、存在しない場合は外部サービスを利用して取得します。
        /// 外部サービスから取得した場合は、データベースにキャッシュとして保存されます。
        /// </summary>
        /// <param name="id">取得する商品のID。</param>
        /// <returns>指定された商品のデータ。見つからない場合はnull。</returns>
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            // キャッシュ、データベース、外部サービスの順にチェック
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                product = await _externalProductService.FetchProductAsync(id);
                if (product is not null)
                {
                    await _productRepository.AddAsync(product); // キャッシュに保存
                }
            }
            return product;
        }

        /// <summary>
        /// データベース内のすべての商品を取得します。
        /// </summary>
        /// <returns>すべての商品のリスト。</returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
