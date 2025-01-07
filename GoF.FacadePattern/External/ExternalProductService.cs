using GoF.FacadePattern.Models;

namespace GoF.FacadePattern.External
{
    /// <summary>
    /// 外部商品サービスのインターフェース。
    /// 外部APIから商品のデータを取得します。
    /// </summary>
    public interface IExternalProductService
    {
        /// <summary>
        /// 指定されたIDの商品を外部APIから取得します。
        /// </summary>
        /// <param name="id">取得する商品のID。</param>
        /// <returns>取得した商品のデータ。見つからない場合はnull。</returns>
        Task<Product?> FetchProductAsync(int id);
    }

    /// <summary>
    /// 外部商品サービスの実装。
    /// 外部APIをモックして商品データを返します。
    /// </summary>
    public class ExternalProductService : IExternalProductService
    {
        /// <summary>
        /// 指定されたIDの商品を外部APIから取得します（モック実装）。
        /// </summary>
        /// <param name="id">取得する商品のID。</param>
        /// <returns>モックされた商品のデータ。</returns>
        public Task<Product?> FetchProductAsync(int id)
        {
            // 外部API呼び出しのモック
            return Task.FromResult(new Product { Id = id, Name = $"External Product {id}", Price = 99.99m })!;
        }
    }
}
