using GoF.FacadePattern.Models;

namespace GoF.FacadePattern.Repositories
{
    /// <summary>
    /// 商品情報を管理するリポジトリインターフェース。
    /// データベース操作に必要なメソッドを定義します。
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// 指定されたIDの商品を取得します。
        /// </summary>
        /// <param name="id">取得する商品のID。</param>
        /// <returns>指定された商品のデータ。見つからない場合はnull。</returns>
        Task<Product?> GetByIdAsync(int id);

        /// <summary>
        /// すべての商品を取得します。
        /// </summary>
        /// <returns>すべての商品のリスト。</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// 商品をデータベースに追加します。
        /// </summary>
        /// <param name="product">追加する商品のデータ。</param>
        /// <returns>非同期操作のタスク。</returns>
        Task AddAsync(Product product);
    }

    /// <summary>
    /// メモリ内データベースを模倣した商品リポジトリの実装。
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new(); // メモリ内データベースのモック

        /// <summary>
        /// 指定されたIDの商品を取得します。
        /// </summary>
        /// <param name="id">取得する商品のID。</param>
        /// <returns>指定された商品のデータ。見つからない場合はnull。</returns>
        public Task<Product?> GetByIdAsync(int id)
        {
            return Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
        }

        /// <summary>
        /// すべての商品を取得します。
        /// </summary>
        /// <returns>すべての商品のリスト。</returns>
        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Product>>(_products);
        }

        /// <summary>
        /// 商品をメモリ内データベースに追加します。
        /// </summary>
        /// <param name="product">追加する商品のデータ。</param>
        /// <returns>非同期操作のタスク。</returns>
        public Task AddAsync(Product product)
        {
            _products.Add(product);
            return Task.CompletedTask;
        }
    }
}
